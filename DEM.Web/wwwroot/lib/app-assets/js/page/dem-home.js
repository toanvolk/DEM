var demIndex = {
    actionType: {
        ToCategoryPage: "to-category-page",
        ToExpensePage: "to-expense-page",
        AddExpense: "add-expense"
    },
    clickEvent: function (e, actionType) {
        let _handle = _demHandle();
        if (actionType == demIndex.actionType.ToCategoryPage) demIndex.toCategoryPage(e, _handle);
        if (actionType == demIndex.actionType.ToExpensePage) demIndex.toExpensePage(e, _handle);
        if (actionType == demIndex.actionType.AddExpense) demIndex.addExpense(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = _demHandle();        
    },
    //children event
    toCategoryPage: function (e, handle) {
        let _data = $(e).closest('.dem-root-category').data();
        let _url = '/category?rootCategoryType=' + _data.rootCategoryType;
        open(_url);
    },
    toExpensePage: function (e, handle) {
        let _data = $(e).closest('.dem-root-category').data();
        let _url = '/expense?rootCategoryType=' + _data.rootCategoryType;
        open(_url);
    },
    addExpense: function (e, handle) {
        let _data = $(e).closest('.dem-root-category').data();
        helper.showDialog({
            contentData: {
                url: "/expense/add",
                data: {
                    categoryType: _data.rootCategoryType
                }
            },
            config: {
                title: "TẠO GIAO DỊCH",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    //this.center();
                    $('.app-materialize select').formSelect().change();
                    $('.app-materialize .datepicker').datepicker({
                        format: 'dd/mm/yyyy',
                        defaultDate: new Date(),
                        setDefaultDate: true
                    });
                    $('.decimal-inputmask').inputmask("decimal", {
                        placeholder: "0",
                        digits: 0,
                        digitsOptional: false,
                        radixPoint: ".",
                        groupSeparator: ",",
                        autoGroup: true,
                        allowPlus: false,
                        allowMinus: true,
                        clearMaskOnLostFocus: false,
                        removeMaskOnSubmit: true
                    });
                    
                },
                width: 920
            }
        });
    }
}
let _demHandle = function () {    
    let _loadCategorys = function (rootCategoryType,callback) {
        let _url = "/home/loaddatas"
        $.get(_url, { rootCategoryType: rootCategoryType }, function (res) {
            if (res.statu == 200) {
                callback(res.data);
            }        
        });
    }
    return {
        loadCategorys: _loadCategorys
    }
}