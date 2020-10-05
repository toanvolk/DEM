
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
        if (actionType == categoryIndex.actionType.Add) {
            let _url = "/category/add";
            $.get(_url, function (res) {
                helper.showDialog(res);
            });            
        }
    },
}

//
categoryIndex.init();