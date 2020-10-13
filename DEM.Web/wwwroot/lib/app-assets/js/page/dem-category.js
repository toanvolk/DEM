var categoryIndex = {
    actionType: {
        Add: "add",
        EditForm: "form-edit",
        Delete: "delete",
        UpdateStatu: "update-statu"
    },
    init: function () {
        let _rootCategoryType = $('input[name=RootCategoryType]').val();
        let _url = "/category/loadDatas";
        $('.grid').kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: _url,
                        dataType: "json",
                        type: "GET",
                        data: {
                            rootCategoryType: _rootCategoryType
                        }
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                },
                serverPaging: false
            },
            pageable: false,
            columns: [
                {
                    field: "index",
                    title: "#",
                    width: 35
                },
                {
                    field: "name",
                    title: "Loại",
                    width: "15%"
                },
                {
                    field: "description",
                    title: "Mô tả"
                },
                {
                    field: "NotUse",
                    title: "Status",
                    template: function (item) {
                        let _html = `<div class="onoffswitch">
                                    <input type = "checkbox" name = "onoffswitch" class="onoffswitch-checkbox" id = "oow-{{id}}" onchange="categoryIndex.changeEvent(this, categoryIndex.actionType.UpdateStatu)" {{checked}} data-id="{{id}}">
                                    <label class="onoffswitch-label" for="oow-{{id}}">
                                        <span class="onoffswitch-inner"></span>
                                        <span class="onoffswitch-switch"></span>
                                    </label>
                                    </div >`
                        return _html
                            .replace(RegExp("{{id}}", "gi"), item.id)
                            .replace(RegExp("{{checked}}", "gi"), item.notUse == true ? "" : "checked");
                    },
                    width: "120px"
                },
                {
                    field: "",
                    width: "15%",
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="categoryIndex.clickEvent(this, categoryIndex.actionType.EditForm)" data-id="' + item.id + '"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="categoryIndex.clickEvent(this, categoryIndex.actionType.Delete)" data-id="' + item.id + '" data-name="' + item.name + '"><i class="ft-trash-2"></i> Xóa</button>';
                        return _html;
                    },
                }
            ]
        })
    },
    loadTable: function () {
        $('.grid').data('kendoGrid').dataSource.read();
    },
    clickEvent: function (e, actionType) {
        let _handle = categoryHandle();
        if (actionType == categoryIndex.actionType.Add) categoryIndex.showFormAdd(e, _handle);
        if (actionType == categoryIndex.actionType.EditForm) categoryIndex.showFormEdit(e, _handle);
        if (actionType == categoryIndex.actionType.Delete) categoryIndex.deleteComfirm(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = categoryHandle();
        if (actionType == categoryIndex.actionType.UpdateStatu) categoryIndex.changeStatu(e, _handle);
    },
    //children event
    showFormAdd: function (e, handle) {
        helper.showDialog({
            contentData: {
                url: "/category/add",
                data: {
                    rootCategoryType: $('input[name=RootCategoryType]').val()
                }
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                close: function () {categoryIndex.loadTable();},
                refresh: function () { categoryIndex.loadTable();}
            }
        });
    },
    showFormEdit: function (e, handle) {
        handle.openFormEdit($(e).data("id"), function (res) {
            helper.showDialog({
                contentData: {
                    url: "/category/EditForm",
                    data: {
                        categoryId: $(e).data("id")
                    }
                },
                config: {
                    title: "CẬP NHẬT",
                    actions: ["Refresh", "Close"],
                    close: function () { categoryIndex.loadTable(); },
                    refresh: function () { categoryIndex.loadTable(); }
                }
            });
        });
    },
    deleteComfirm: function (e, handle) {
        var _dataCategory = $(e).data();
        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _dataCategory.name + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                handle.deleteData(_dataCategory.id, function (res) {
                    swal('Đã xóa!', res.message, 'success');
                    $('.grid').data('kendoGrid').dataSource.read();
                });
            }

        }).catch(swal.noop);
    },
    changeStatu: function (e, handle) {
        let _categoryId = $(e).data("id");
        handle.changeStatu(_categoryId, $(e).prop('checked'), function (res) { });
    }
}
var categoryHandle = function () {
    let _openFormEdit = function (categoryId, callback) {
        let _url = "/category/editform";
        $.get(_url, { categoryId: categoryId }, function (res) {
            callback(res);
        });
    }
    let _deleteData = function (categoryId, callback) {
        let _url = "/category/delete";
        $.post(_url, { categoryId: categoryId }, function (res) {
            callback(res);
        });
    }
    let _changeStatu = function (categoryId, notUse, callback) {
        let _url = "/category/changeStatu"
        $.post(_url, { categoryId: categoryId, notUse: notUse }, function (res) {
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
categoryIndex.init();