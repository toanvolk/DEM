
var categoryIndex = {
    actionType: {
        Add: "add"
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
                    field: "",
                    title: "Action",
                    width: "10%"
                }
            ]
        })
    },
    clickEvent: function (e, actionType) {
        let _handle = categoryHandle();
        if (actionType == categoryIndex.actionType.Add) categoryIndex.showFormAdd(e, _handle);
    },
    //children event
    showFormAdd: function (e, handle) {
        handle.openFormAdd(function (res) {
            helper.showDialog({
                content: res,
                title: "TẠO MỚI"
            });
        });
    }
}
var categoryHandle = function () {
    let _openFormAdd = function (callback) {
        let _url = "/category/add";
        $.get(_url, function (res) {
            callback(res);
        });
    }
    return {
        openFormAdd: _openFormAdd
    }
}
//
categoryIndex.init();