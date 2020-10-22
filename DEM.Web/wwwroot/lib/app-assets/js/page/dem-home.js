var demIndex = {
    actionType: {
        ToCategoryPage: "to-category-page",
        ToExpensePage: "to-expense-page",
        AddExpense: "add-expense",
        BuildIntended: "build-intended"
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
    rDaily: function (dailys, moneys) {
        /*************************************************
        *               Quarterly Sales Stats               *
        *************************************************/

        var quarterlySales = new Chartist.Bar('#quarterly-sales', {
            labels: dailys,
            series: [
                moneys
            ]
        }, {
            axisY: {
                labelInterpolationFnc: function (value) {
                    return (value / 1000) + 'k';
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
                    pointClass: 'ct-point'
                })
            ]
        });
        quarterlySales.on('draw', function (data) {
            if (data.type === 'bar') {
                data.element.attr({
                    style: 'stroke-width: 10px',
                    y1: 260,
                    x1: data.x1 + 0.001
                });
                data.group.append(new Chartist.Svg('circle', {
                    cx: data.x2,
                    cy: data.y2,
                    r: 5
                }, 'ct-slice-pie'));
            }
        });
        quarterlySales.on('created', function (data) {
            var defs = data.svg.querySelector('defs') || data.svg.elem('defs');
            defs.elem('linearGradient', {
                id: 'barGradient1',
                x1: 0,
                y1: 0,
                x2: 0,
                y2: 1
            }).elem('stop', {
                offset: 0,
                'stop-color': 'rgba(253,99,107,1)'
            }).parent().elem('stop', {
                offset: 1,
                'stop-color': 'rgba(253,99,107, 0.6)'
            });
            return defs;
        });

    },
    init: function () {
        let _dailyInMonthUrl = 'home/getdailyinmonthcurrent_dashboard';
        $.get(_dailyInMonthUrl, {}, function (res) {
            demIndex.rDaily(res.dailys, res.moneys);
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
        loadCategorys: _loadCategorys
    }
}

demIndex.init();
