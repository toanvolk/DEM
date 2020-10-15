var expenseIndex = {
    dom: {
        grid: "#dem-expense .grid"
    },
    actionType: {
        Add: "add",
        EditForm: "form-edit",
        Delete: "delete",
        UpdateStatu: "update-statu"
    },
    init: function () {
        let _categoryId = $('#dem-expense input[name=CategoryId]').val();
        let _url = "/expense/loadData";
        $(expenseIndex.dom.grid).kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: _url,
                        dataType: "json",
                        type: "GET",
                        data: {
                            categoryId: _categoryId
                        }
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                },
                group: [
                    {
                        field: "payerName",
                        aggregates: [
                            { field: "money", aggregate: "sum" }
                        ]
                    },
                    {
                        field: "payTime",
                        aggregates: [
                            { field: "money", aggregate: "sum" }
                        ]
                    },
                ],
                serverPaging: false
            },
            pageable: false,
            groupable: true,
            groupExpand: function (e) {
                for (let i = 0; i < e.group.items.length; i++) {
                    var expanded = e.group.items[i].value
                    e.sender.expandGroup(".k-grouping-row:contains(" + expanded + ")");
                }
            },
            columns: [
                {
                    field: "index",
                    title: "#",
                    width: 35
                },
                {
                    field: "payerName",
                    title: "Người ghi",
                    width: "15%",
                    hidden: true,
                    groupHeaderTemplate: "Người thực hiện: #= value # - Tổng: #= kendo.toString(aggregates.money.sum, '\\#\\#,\\#') #"
                },
                {
                    field: "payTime",
                    title: "Thời gian",
                    hidden: true,
                    //template: "#= kendo.toString(kendo.parseDate(payTime, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                    width: "15%",
                    groupHeaderTemplate: "Ngày : #= kendo.toString(kendo.parseDate(value, 'yyyy-MM-dd'), 'dd/MM/yyyy') # - Tổng: #= kendo.toString(aggregates.money.sum, '\\#\\#,\\#') #"
                },
                {
                    field: "description",
                    title: "Mô tả"
                },
                {
                    field: "money",
                    title: "Số tiền",
                    width: "15%",
                    template: "#= kendo.toString(money, '\\#\\#,\\#') #"
                },
                {
                    field: "",
                    width: "15%",
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="expenseIndex.clickEvent(this, expenseIndex.actionType.EditForm)" data-id="' + item.id + '"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="expenseIndex.clickEvent(this, expenseIndex.actionType.Delete)" data-id="' + item.id + '" data-name="' + item.name + '"><i class="ft-trash-2"></i> Xóa</button>';
                        return _html;
                    },
                }
            ]
        })
    },
    loadTable: function () {
        $(expenseIndex.dom.grid).data('kendoGrid').dataSource.read();
    },
    clickEvent: function (e, actionType) {
        let _handle = expenseHandle();
        if (actionType == expenseIndex.actionType.Add) expenseIndex.showFormAdd(e, _handle);
        if (actionType == expenseIndex.actionType.EditForm) expenseIndex.showFormEdit(e, _handle);
        if (actionType == expenseIndex.actionType.Delete) expenseIndex.deleteComfirm(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = expenseHandle();
        if (actionType == expenseIndex.actionType.UpdateStatu) expenseIndex.changeStatu(e, _handle);
    },
    //children event
    showFormAdd: function (e, handle) {
        let _data = $(e).data();
        helper.showDialog({
            contentData: {
                url: "/expense/add",
                data: {
                    categoryId: _data.categoryData.id
                }
            },
            config: {
                title: "TẠO GIAO DỊCH",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    this.center();
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
                close: function () { },
                refresh: function () { }
            }
        });
    },
    showFormEdit: function (e, handle) {
        handle.openFormEdit($(e).data("id"), function (res) {
            helper.showDialog({
                contentData: {
                    url: "/expense/EditForm",
                    data: {
                        expenseId: $(e).data("id")
                    }
                },
                config: {
                    title: "CẬP NHẬT",
                    actions: ["Refresh", "Close"],
                    close: function () { expenseIndex.loadTable(); },
                    refresh: function () { expenseIndex.loadTable(); }
                }
            });
        });
    },
    deleteComfirm: function (e, handle) {
        var _dataexpense = $(e).data();
        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _dataexpense.name + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                handle.deleteData(_dataexpense.id, function (res) {
                    swal('Đã xóa!', res.message, 'success');
                    $(expenseIndex.dom.grid).data('kendoGrid').dataSource.read();
                });
            }

        }).catch(swal.noop);
    },
    changeStatu: function (e, handle) {
        let _expenseId = $(e).data("id");
        handle.changeStatu(_expenseId, $(e).prop('checked'), function (res) { });
    }
}
var expenseHandle = function () {
    let _openFormEdit = function (expenseId, callback) {
        let _url = "/expense/editform";
        $.get(_url, { expenseId: expenseId }, function (res) {
            callback(res);
        });
    }
    let _deleteData = function (expenseId, callback) {
        let _url = "/expense/delete";
        $.post(_url, { expenseId: expenseId }, function (res) {
            callback(res);
        });
    }
    let _changeStatu = function (expenseId, notUse, callback) {
        let _url = "/expense/changeStatu"
        $.post(_url, { expenseId: expenseId, notUse: notUse }, function (res) {
            callback(res);
        });
    }
    return {
        openFormEdit: _openFormEdit,
        deleteData: _deleteData,
        changeStatu: _changeStatu
    }
}
//
expenseIndex.init();