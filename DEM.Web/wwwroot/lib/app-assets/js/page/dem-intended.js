var intendedIndex = {
    dom: {
        demIntended: "#dem-intended",
        grid: "#dem-intended .grid"
    },
    actionType: {
        Add: "add",
        EditForm: "edit-form",
        Delete: "delete"
    },
    clickEvent: function (e,actionType) {
        let _$root = $(e).closest(intendedIndex.dom.demIntended);
        let _handle = _intendedHandle();

        if (actionType == intendedIndex.actionType.Add) intendedIndex.add(e, _handle, _$root);
        if (actionType == intendedIndex.actionType.EditForm) intendedIndex.editForm(e, _handle, _$root);
        if (actionType == intendedIndex.actionType.Delete) intendedIndex.delete(e, _handle, _$root);
    },
    changeEvent: function (e, actionType) {

    },
    dateRangeChanged: function (start, end) {
        let _handle = _intendedHandle();
        let _$root = $(intendedIndex.dom.demIntended);        
        let _rootCategory = _$root.data().rootCategoryType;

        let _source = _handle.getDataSource(_rootCategory, start._d.toJSON(), end._d.toJSON());

        $(intendedIndex.dom.grid).data('kendoGrid').setDataSource(_source);
    },
    //event child
    add: function (e, handle, root) {
        let _data = root.data();
        handle.showDialog({
            contentData: {
                url: "/intended/add",
                data: {
                    rootCategory: _data.rootCategoryType
                }
            },            
            config: {
                title: "TẠO KẾ HOẠCH",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    $('section#dem-intended-add .shawCalRanges').daterangepicker({
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
                close: function () { intendedIndex.loadTable(); },
                refresh: function () { intendedIndex.loadTable(); },
                width: 600
            }
        });
    },
    init: function (e, handle, root) {
        $(intendedIndex.dom.grid).kendoGrid({
            pageable: true,
            columns: [
                {
                    field: "index",
                    title: "#",
                    width: 35
                },
                {
                    field: "fromDate",
                    title: "Từ ngày",
                    template: "#= moment(fromDate).format('DD/MM/YYYY') #",
                    width: "15%"
                },
                {
                    field: "toDate",
                    title: "Đến ngày",
                    template: "#= moment(toDate).format('DD/MM/YYYY') #",
                    width: "15%"
                },
                {
                    field: "description",
                    title: "Mô tả"
                },
                {
                    field: "",
                    width: "15%",
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="intendedIndex.clickEvent(this, intendedIndex.actionType.EditForm)" data-id="' + item.id + '"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="intendedIndex.clickEvent(this, intendedIndex.actionType.Delete)" data-id="' + item.id + '" data-description="' + item.description + '"><i class="ft-trash-2"></i> Xóa</button>';
                        return _html;
                    },
                }
            ]
        });
        $('.shawCalRanges').change();
    },
    editForm: function (e, handle, root) {
        let _id = $(e).data('id');
        handle.showDialog({
            contentData: {
                url: "/intended/edit",
                data: {
                    intendedId: _id
                }
            },
            config: {
                title: "CẬP NHẬT KẾ HOẠCH",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    let _dataTime = $('section#dem-intended-edit .shawCalRanges').data();
                    $('section#dem-intended-edit .shawCalRanges').daterangepicker({
                        startDate: moment(_dataTime.fromDate),
                        endDate: moment(_dataTime.endDate),
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
                close: function () { intendedIndex.loadTable(); },
                refresh: function () { intendedIndex.loadTable(); },
                width: 600
            }
        });
    },
    delete: function (e, handle, root) {
        var _dataIntended = $(e).data();
        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _dataIntended.description + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                handle.delete(_dataIntended.id, function (res) {
                    swal('Đã xóa!', res.message, 'success');
                    intendedIndex.loadTable();
                });
            }

        }).catch(swal.noop);
    },
    //-------------------function
    loadTable: function () {
        $(intendedIndex.dom.grid).data('kendoGrid').dataSource.read();
    }
}
let _intendedHandle = function () {
    let _getDataSource = function (rootCategory, startTime, endTime) {
        let _url = "/intended/loadData";
        let _source = new kendo.data.DataSource({
            transport: {
                read: {
                    url: _url,
                    dataType: "json",
                    type: "GET",
                    data: {
                        rootCategory: rootCategory,
                        startTime: startTime,
                        endTime: endTime
                    }
                }
            },
            schema: {
                data: "data",
                total: "total"
            },
            serverPaging: true,
            serverFiltering: true,
            pageSize: 10,
        });    
        return _source;
    }
    let _delete = function (id, callback) {
        let _url = 'intended/delete';
        $.post(_url, { id: id }, function (res) {
            callback(res);
        });
    }
    return {
        getDataSource: _getDataSource,
        showDialog: helper.showDialog,
        delete: _delete
    }
}
intendedIndex.init();