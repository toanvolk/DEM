var expenseIndex= {
    dataSource:[],
    actionType: {
        MapDescriptionInput: 'map-expense',
        MapPayerInput: 'map-payer',
        AddData: 'add-data'
    },
    initForm: function () {
        let _handle = _expenseHandle();        
        expenseIndex.createTable(_handle);
    },
    clickEvent: function (e, actionType) {
        let _handle = _expenseHandle();
        if (actionType == expenseIndex.actionType.AddData) expenseIndex.addData(e, _handle);
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
            obj.PayTime = new Date(_dateString[2], _dateString[1] - 1, _dateString[0]);
            obj.Money = parseFloat(obj.Money.replaceAll(',', ''));
            obj.Id = handle.newId();

            handle.addItem(expenseIndex.dataSource, obj);
            expenseIndex.createorRefreshTable();
            console.log(expenseIndex.dataSource);
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
                { field: "Description", title: 'Mô tả', width: '70%' },
                {
                    field: "Money", title: 'Số tiền',
                    footerTemplate: "<div class='text-right'>Tổng: #: kendo.toString(sum, '\\#\\#,\\#') # </div>",
                    format: "{0:##,#}",
                    attributes: { class: "text-right" }
                }
            ],
            dataSource: _dataSource,
            
        });
    }
}
let _expenseHandle = function () {
    let _addItem = function (source, obj) {
        source.push(obj);
    }
    let _removeItem = function (source,id) {
        source = $.grep(source, function (e) {
            return e.id != id;
        });
    }

    return {
        data: helper.formData,
        newId: helper.createGUID,
        addItem: _addItem,
        removeItem: _removeItem
    }
}