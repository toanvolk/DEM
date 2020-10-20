var intendedIndex = {
    dom: {
        demIntended: "#dem-intended"
    },
    actionType: {
        Add: "add"
    },
    clickEvent: function (e,actionType) {
        let _$root = $(e).closest(intendedIndex.dom.demIntended);
        let _handle = _intendedHandle();

        if (actionType == intendedIndex.actionType.Add) intendedIndex.add(e, _handle, _$root);
    },
    changeEvent: function (e, actionType) {

    },
    dateRangeChanged: function (start, end) {
        //let _handle = expenseHandle();
        //expenseIndex.loadTable(_handle.dataSourceKendo(
        //    $(expenseIndex.dom.rootCategoryType).val(),
        //    start._d.toJSON(),
        //    end._d.toJSON())
        //);
    },
    //event child
    add: function (e, handle) {
        let _data = $(e).closest('.dem-root-category').data();
        helper.showDialog({
            contentData: {
                url: "/intended/add",
                //data: {
                //    rootCategory: _data.rootCategoryType
                //}
            },

            config: {
                title: "TẠO KẾ HOẠCH",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    $('.shawCalRanges').daterangepicker({
                        //startDate: moment().subtract('days', 7),
                        ranges: {
                            'Today': [moment(), moment()],
                            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                            'This Month': [moment().startOf('month'), moment().endOf('month')],
                            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                        },
                        alwaysShowCalendars: true,
                        locale: {
                            format: 'DD/MM/YYYY'
                        }
                    },
                        function (start, end) {
                            //return intendedAdd.dateRangeChanged(start, end);
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
                width: 600
            }
        });
    }
}
let _intendedHandle = function () {

}