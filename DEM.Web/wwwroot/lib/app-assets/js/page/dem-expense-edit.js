var expenseEditIndex= {
    dataSource:[],
    actionType: {
        MapDescriptionInput: 'map-expense',
        MapPayerInput: 'map-payer',
        SaveData: 'save-data',
        CategoryChange: "category-change"
    },
    clickEvent: function (e, actionType) {
        let _handle = _expenseEditHandle();
        if (actionType == expenseEditIndex.actionType.SaveData) expenseEditIndex.saveData(e, _handle);
        if (actionType == expenseEditIndex.actionType.CategoryChange) expenseEditIndex.categoryChange(e, _handle);

    },
    changeEvent: function (e, actionType) {
        let _handle = _expenseEditHandle();
        if (actionType == expenseEditIndex.actionType.MapDescriptionInput) expenseEditIndex.mapDescriptionInput(e, _handle);
        if (actionType == expenseEditIndex.actionType.MapPayerInput) expenseEditIndex.mapPayerInput(e, _handle);
    },
    mapDescriptionInput: function (e, handle) {
        $(e).next().val($(e).val());
    },
    mapPayerInput: function (e, handle) {
        $(e).closest('.select-wrapper').next().val($(e).val());
    },
    saveData: function (e, handle) {
        let _data = handle.data.inputToObject($(e).closest('section#dem-expense-edit'), function (obj) {
            let _dateString = obj.PayTime.split("/")            
            obj.PayTime = new Date(_dateString[2] + '/' + _dateString[1] + '/' + _dateString[0]).toJSON();
            obj.Money = parseFloat(obj.Money.replaceAll(',', ''));
        });
        console.log(_data);
        handle.saveData(_data, function (res) {
            if (res.statu == 200) {
                handle.closeDialog($(e).closest('section#dem-expense-edit'));
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
    categoryChange: function (e, handle) {
        let _$demChips = $(e).closest('.dem-chips');
        _$demChips.find('.chip').removeClass('active');
        $(e).addClass('active');

        //set value
        let _categoryId = $(e).data('id');
        let _$container = $(e).closest('section#dem-expense-edit');
        _$container.find('input[name=CategoryId]').last().val(_categoryId);
    }
}
let _expenseEditHandle = function () {   
    let _saveData = function (data,callback) {
        let _url = 'expense/update';
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }
    return {
        data: helper.formData,
        saveData: _saveData,
        closeDialog: helper.closeDialog,
        validate: helper.inputValidate.checkRequired
    }
}