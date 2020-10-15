var expenseAddIndex= {
    dataSource:[],
    actionType: {
        MapDescriptionInput: 'map-expense',
        MapPayerInput: 'map-payer',
        AddData: 'add-data',
        SaveData: 'save-data',
        DeleteItem: 'delete-item'
    },
    initForm: function () {
        let _handle = _expenseAddHandle();        
        expenseAddIndex.dataSource = [];
    },
    clickEvent: function (e, actionType) {
        let _handle = _expenseAddHandle();
        if (actionType == expenseAddIndex.actionType.AddData) expenseAddIndex.addData(e, _handle);
        if (actionType == expenseAddIndex.actionType.SaveData) expenseAddIndex.saveData(e, _handle);
        if (actionType == expenseAddIndex.actionType.DeleteItem) expenseAddIndex.deleteItem(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = _expenseAddHandle();
        if (actionType == expenseAddIndex.actionType.MapDescriptionInput) expenseAddIndex.mapDescriptionInput(e, _handle);
        if (actionType == expenseAddIndex.actionType.MapPayerInput) expenseAddIndex.mapPayerInput(e, _handle);
    },
    mapDescriptionInput: function (e, handle) {
        $(e).next().val($(e).val());
    },
    mapPayerInput: function (e, handle) {
        $(e).closest('.select-wrapper').next().val($(e).val());
    },
    
    addData: function (e, handle) {
        if (!handle.validate($(e).closest('section#dem-expense-add'))) return;
        handle.data.inputToObject($(e).closest('section#dem-expense-add'), function (obj) {
            let _dateString = obj.PayTime.split("/")
            obj.PayTime = new Date(_dateString[2], _dateString[1] - 1, _dateString[0]).toJSON();
            obj.Money = parseFloat(obj.Money.replaceAll(',', ''));
            obj.Id = handle.newId();

            handle.addItem(expenseAddIndex.dataSource, obj);
            expenseAddIndex.createorRefreshTable();

            if (expenseAddIndex.dataSource.length > 0)
                $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().show();
            else
                $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().hide();
        });
    },
    createorRefreshTable: function () {
        let _dataSource = new kendo.data.DataSource({
            data: expenseAddIndex.dataSource,
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
                        let _html = '<button type="button" class="btn btn-small btn-outline-danger round" onclick="expenseAddIndex.clickEvent(this, expenseAddIndex.actionType.DeleteItem)" data-id="' + item.Id + '"><i class="ft-trash-2"></i></button>';
                        return _html;
                    },
                }

            ],
            dataSource: _dataSource,
            
        });
    },

    saveData: function (e, handle) {
        handle.saveData(expenseAddIndex.dataSource, function (res) {
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
        expenseAddIndex.dataSource = handle.removeItem(expenseAddIndex.dataSource, _id);
        if (expenseAddIndex.dataSource.length == 0) $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().hide();
        expenseAddIndex.createorRefreshTable();
    }
}
let _expenseAddHandle = function () {
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
        closeDialog: helper.closeDialog,
        validate: helper.inputValidate.checkRequired
    }
}