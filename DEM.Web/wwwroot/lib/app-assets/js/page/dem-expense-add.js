var expenseIndex= {
    dataSource:[],
    actionType: {
        MapDescriptionInput: 'map-expense',
        MapPayerInput: 'map-payer',
        AddData: 'add-data',
        SaveData: 'save-data',
        DeleteItem: 'delete-item'
    },
    initForm: function () {
        let _handle = _expenseHandle();        
        expenseIndex.dataSource = [];
    },
    clickEvent: function (e, actionType) {
        let _handle = _expenseHandle();
        if (actionType == expenseIndex.actionType.AddData) expenseIndex.addData(e, _handle);
        if (actionType == expenseIndex.actionType.SaveData) expenseIndex.saveData(e, _handle);
        if (actionType == expenseIndex.actionType.DeleteItem) expenseIndex.deleteItem(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = _expenseHandle();
        if (actionType == expenseIndex.actionType.MapDescriptionInput) expenseIndex.mapDescriptionInput(e, _handle);
        if (actionType == expenseIndex.actionType.MapPayerInput) expenseIndex.mapPayerInput(e, _handle);
    },
    mapDescriptionInput: function (e, handle) {
        $(e).next().val($(e).val());
    },
    mapPayerInput: function (e, handle) {
        $(e).closest('.select-wrapper').next().val($(e).val());
    },
    
    addData: function (e, handle) {
        handle.data.inputToObject($(e).closest('section#dem-expense-add'), function (obj) {
            let _dateString = obj.PayTime.split("/")
            obj.PayTime = new Date(_dateString[2], _dateString[1] - 1, _dateString[0]).toJSON();
            obj.Money = parseFloat(obj.Money.replaceAll(',', ''));
            obj.Id = handle.newId();

            handle.addItem(expenseIndex.dataSource, obj);
            expenseIndex.createorRefreshTable();

            if (expenseIndex.dataSource.length > 0)
                $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().show();
            else
                $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().hide();
        });
    },
    createorRefreshTable: function () {
        let _dataSource = new kendo.data.DataSource({
            data: expenseIndex.dataSource,
            aggregate: [
                { field: "Money", aggregate: "sum" }
            ]
        });
        let _grid = $('section#dem-expense-add .expense-grid').data('kendoGrid');
        if (_grid) {
            _grid.dataSource.read(_dataSource);
        }
        $('section#dem-expense-add .expense-grid').kendoGrid({
            columns: [
                { field: "Description", title: 'Mô tả', width: '60%' },
                {
                    field: "Money", title: 'Số tiền',
                    footerTemplate: "<div class='text-right'>Tổng: #: kendo.toString(sum, '\\#\\#,\\#') # </div>",
                    format: "{0:##,#}",
                    attributes: { class: "text-right" }
                },
                {
                    field: "",
                    width: '15%',
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-small btn-outline-danger round" onclick="expenseIndex.clickEvent(this, expenseIndex.actionType.DeleteItem)" data-id="' + item.Id + '"><i class="ft-trash-2"></i></button>';
                        return _html;
                    },
                }

            ],
            dataSource: _dataSource,
            
        });
    },

    saveData: function (e, handle) {
        handle.saveData(expenseIndex.dataSource, function (res) {
            if (res.statu == 200) {
                handle.closeDialog($(e).closest('section#dem-expense-add'));
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
    deleteItem: function (e, handle) {
        let _id = $(e).data('id');
        expenseIndex.dataSource = handle.removeItem(expenseIndex.dataSource, _id);
        if (expenseIndex.dataSource.length == 0) $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().hide();
        expenseIndex.createorRefreshTable();
    }
}
let _expenseHandle = function () {
    let _addItem = function (source, obj) {
        source.push(obj);
    }
    let _removeItem = function (source,id) {
        source = $.grep(source, function (e) {
            return e.Id != id;
        });
        return source;
    }
    let _saveData = function (data,callback) {
        let _url = 'expense/create';
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }
    return {
        data: helper.formData,
        newId: helper.createGUID,
        addItem: _addItem,
        removeItem: _removeItem,
        saveData: _saveData,
        closeDialog: helper.closeDialog
    }
}