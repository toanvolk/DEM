var demIndex = {
    actionType: {
        Add: "add",
        ShowCaption: "show-caption",
        GetDescription: "get-description",
        DirectToCategoryPage: "to-category-page",
        AddExpense: "add-expense"
    },
    clickEvent: function (e, actionType) {
        let _handle = _demHandle();
        if (actionType == demIndex.actionType.ShowCaption) demIndex.showCaption(e, _handle);
        if (actionType == demIndex.actionType.Add) demIndex.showFormAdd(e, _handle);
        if (actionType == demIndex.actionType.DirectToCategoryPage) demIndex.directToCategoryPage(e, _handle);
        if (actionType == demIndex.actionType.AddExpense) demIndex.addExpense(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = _demHandle();
        if (actionType == demIndex.actionType.GetDescription) demIndex.getDescription(e, _handle);
    },
    //children event
    showFormAdd: function (e, handle) {
        let _rootCategoryType = $('#dem-category-for').data('setup')['rootCategoryType'];
        helper.showDialog({
            contentData: {
                url: "/category/add",
                data: {
                    rootCategoryType: _rootCategoryType
                }
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                close: function () {
                    let _idDom = _rootCategoryType.toLowerCase();
                    $('#' + _idDom + '.dem-root-category').click();
                },
                refresh: function () {
                    let _idDom = _rootCategoryType.toLowerCase();
                    $('#' + _idDom + '.dem-root-category').click();
                }
            }
        });
    },
    showCaption: function (e, handle) {
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
                    //addition root-category
                    item['rootCategoryType'] = _data.rootCategoryType;
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


                    $('.dem-category-card').append($(_template).data('categoryData', item));
                });
            }
        });
    },
    directToCategoryPage: function (e, handle) {
        let _data = $('#dem-category-for').data('setup');
        let _url = '/category?rootCategoryType=' + _data.rootCategoryType;
        open(_url);
    },
    getDescription: function (e, handle) {
        let _value = $(e).val();
        $(e).next().val(_value);
        console.log(_value);
    },
    addExpense: function (e, handle) {
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
                close: function () {},
                refresh: function () {}
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