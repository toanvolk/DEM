var demIndex = {
    actionType: {
        Add: "add",
        ShowCaption: "show-caption",
        CreateCategory: "create-category",
        GetDescription: "get-description"
    },
    clickEvent: function (e, actionType) {
        if (actionType == demIndex.actionType.ShowCaption) {
            let _caption = $(e).data('caption');
            let _handle = _demHandle();

            $('.dem-category .dem-category-title').text(_caption);

            //map data
            let _data = $(e).data();
            $('#dem-category-for').data('setup', _data);

            //clear item old
            $('.dem-category-card .dem-category-item').remove();

            //load data
            _handle.loadCategorys(_data.rootCategoryType, function (data) {
                if (Array.isArray(data)) {
                    data.forEach(function (item) {
                        //Get template
                        let _template = $('#dem-category-template').html();

                        //Map style of RootCategory
                        let _styleRegex = new RegExp("{{style}}", "gi");
                        _template = _template.replace(_styleRegex, _data.style);

                        //Map icon of RootCategory
                        let _iconRegex = new RegExp("{{icon}}", "gi");
                        _template = _template.replace(_iconRegex, _data.iconCategory);

                        //Map data
                        let _categoryName = new RegExp("{{categoryName}}", "gi");
                        let _description = new RegExp("{{description}}", "gi");

                        _template = _template.replace(_categoryName, item.name);
                        _template = _template.replace(_description, item.description);

                        $('.dem-category-card').append(_template);
                    });
                }
            });
        }
        if (actionType == demIndex.actionType.Add) {
            $('#dem-add-item-modal .dem-modal-title').text('TẠO MỚI LOẠI' + ' - ' + $('.dem-category .dem-category-title').text());  
        }
        if (actionType == demIndex.actionType.CreateCategory) {
            let _jsonHandle = objectJsonHandle();
            let _handle = _demHandle();

            let _data = _jsonHandle.mappingInputToObject('#dem-add-item-modal');
            let _rootCategoryData = $('#dem-category-for').data('setup');
            _data.Type = _rootCategoryData.rootCategoryType;
            _handle.createCategory(_data, function () {
                let _data = $('#dem-category-for').data('setup');
                let _idRootCategory = _data.rootCategoryType.toLowerCase();
                $('#' + _idRootCategory).click();
            });
        }
    },
    changeEvent: function (e, actionType) {
        if (actionType == demIndex.actionType.GetDescription) {
            let _value = $(e).val();
            $(e).next().val(_value);
            console.log(_value);
        }
    }
}
let _demHandle = function () {
    let _createCategory = function(data,callback) {
        let _url = "/category/create";        
        $.post(_url, { category: data }, function (res) {
            if (res.statu == 200) {
                callback();
            }
        });
    }
    let _loadCategorys = function (rootCategoryType,callback) {
        let _url = "/category/loaddatas"
        $.get(_url, { rootCategoryType: rootCategoryType }, function (res) {
            if (res.statu == 200) {
                callback(res.data);
            }        
        });
    }
    return {
        createCategory: _createCategory,
        loadCategorys: _loadCategorys
    }
}