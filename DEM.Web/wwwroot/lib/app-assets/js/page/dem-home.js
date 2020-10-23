var demIndex = {
    actionType: {
        ToCategoryPage: "to-category-page",
        ToExpensePage: "to-expense-page",
        AddExpense: "add-expense",
        BuildIntended: "build-intended",
        ExpendedRealAndIntended: "expended-real-and-intended",
        DailyInMonth: "daily-in-month"
    },
    clickEvent: function (e, actionType) {
        let _handle = _demHandle();
        if (actionType == demIndex.actionType.ToCategoryPage) demIndex.toCategoryPage(e, _handle);
        if (actionType == demIndex.actionType.ToExpensePage) demIndex.toExpensePage(e, _handle);
        if (actionType == demIndex.actionType.AddExpense) demIndex.addExpense(e, _handle);
        if (actionType == demIndex.actionType.BuildIntended) demIndex.buildIntended(e, _handle);
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
    },
    buildIntended: function (e, handle) {
        let _data = $(e).closest('.dem-root-category').data();
        let _url = 'intended?rootCategory=' + _data.rootCategoryType;
        open(_url);
    },
    rDaily: function (dailys, moneys, handle) {
        var data = {
            labels: dailys,
            series: moneys
        };

        var options = {
            axisY: {
                labelInterpolationFnc: function (value) {
                    return handle.formatNumber(value / 1000) + 'k';
                },
                scaleMinSpace: 30,
            },
            axisX: {
                showGrid: false,
                labelInterpolationFnc: function (value, index) {
                    return value;//index % 6 === 0 ? value : null;
                }
            },
            plugins: [
                Chartist.plugins.tooltip({
                    appendToBody: true,
                    pointClass: 'ct-point',
                    currency: true,
                    currencyFormatCallback: function (value, options) {
                        return handle.formatNumber(value);
                    }
                })
            ],
            seriesBarDistance: 10
        };
        let quarterlyDaily = Chartist.Bar('#quarterly-daily'
            , data
            , options
        );
        quarterlyDaily.on('draw', function (data) {
            if (data.type === 'bar') {
                let _colors = ["#fa626b", "#28afd0", "#fdb901"]; //Expense : #fa626b //Revenue: #28afd0 // Saving = #fdb901

                data.element.attr({
                    style: 'stroke-width: 10px; stroke:' + _colors[data.seriesIndex],
                    y1: data.y1,
                    x1: data.x1 + 0.001
                });
            }
        });
    },
    rExpendedRealAndIntended: function (names, moneys, handle) {
        var data = {
            labels: names,
            series: moneys
        };

        var options = {
            axisY: {
                labelInterpolationFnc: function (value) {
                    return handle.formatNumber(value / 1000) + 'k';
                },
                scaleMinSpace: 30,
            },
            axisX: {
                showGrid: false,
                labelInterpolationFnc: function (value, index) {
                    return value;//index % 6 === 0 ? value : null;
                }
            },
            plugins: [
                Chartist.plugins.tooltip({
                    appendToBody: true,
                    pointClass: 'ct-point',
                    currency: true,
                    currencyFormatCallback: function (value, options) {
                        return handle.formatNumber(value);
                    }
                })
            ],
            seriesBarDistance: 10
        };
        let expendedRealAndIntended = Chartist.Bar('#dashboard-expense-real'
            , data
            , options
        );
        expendedRealAndIntended.on('draw', function (data) {
            if (data.type === 'bar') {
                let _colors = ["#fa626b", "#3fc52f"]; //Expense : #fa626b //Intended: #3fc52f

                data.element.attr({
                    style: 'stroke-width: 10px; stroke:' + _colors[data.seriesIndex],
                    y1: data.y1,
                    x1: data.x1 + 0.001
                });
            }
        });
    },
    init: function (tag = "All") {
        let _handle = _demHandle();
        let _dailyInMonthUrl = 'home/getdailyinmonthcurrent_dashboard';
        let _expendedRealAndIntendedUrl = 'home/getexpenserealandintended_dashboard';

        if (tag == "All" || tag == demIndex.actionType.DailyInMonth)
            $.get(_dailyInMonthUrl, {}, function (res) {
                demIndex.rDaily(res.dailys, res.moneys, _handle);
            });
        if (tag == "All" || tag == demIndex.actionType.ExpendedRealAndIntended)
            $.get(_expendedRealAndIntendedUrl, {}, function (res) {
                demIndex.rExpendedRealAndIntended(res.names, res.moneys, _handle);
            });
    }
}
let _demHandle = function () {
    let _loadCategorys = function (rootCategoryType, callback) {
        let _url = "/home/loaddatas"
        $.get(_url, { rootCategoryType: rootCategoryType }, function (res) {
            if (res.statu == 200) {
                callback(res.data);
            }
        });
    }
    return {
        loadCategorys: _loadCategorys,
        formatNumber: helper.formatNumber.addCommas
    }
}

demIndex.init();
