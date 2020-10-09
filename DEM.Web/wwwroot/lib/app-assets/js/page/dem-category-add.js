var categoryAddIndex = {
    actionType: {
        Save: "save",
        MapDescription: "map-description"
    },
    clickEvent: function (e, type) {
        let _handle = categoryAddHandle();
        if (type == categoryAddIndex.actionType.Save) categoryAddIndex.saveData(e, _handle);
    },
    changeEvent: function (e, type) {
        let _handle = categoryAddHandle();
        if (type == categoryAddIndex.actionType.MapDescription) categoryAddIndex.mapDescription(e, _handle);
    },
    //children event
    saveData: function (e, handle) {
        let _$rootSelector = $(e).closest('section#dem-category-add');
        if (!handle.validateInput({
            content: _$rootSelector
        })) return;
        let _data = handle.data.inputToObject(_$rootSelector);
        handle.saveData(_data, function () {
            //clear input
            handle.data.clearInput({
                content: _$rootSelector,
                callback: function (content) {
                    content.find('textarea').val('');
                }
            });
            //show message
            let _htmlMessage = '<label class="dem-message light-green-text" style="padding-left: 5px">Thêm thành công!</label>'
            $(_htmlMessage).insertAfter($(e));
            setTimeout(function () {
                if ($(e).next().hasClass('dem-message')) $(e).next().remove();
            }, 1500);
        });

        console.log(_data);
    },
    mapDescription: function (e, handle) {
        $(e).next().val($(e).val());
    }
}
var categoryAddHandle = function () {
    let _helper = helper;
    let _saveData = function (data, callback) {
        let _url = "/category/create";
        $.post(_url, { category: data }, function (res) {
            if (res.statu == 200) {
                callback();
            }
        });
    }
    return {
        data: _helper.formData,
        validateInput: _helper.inputValidate.checkRequired,
        saveData: _saveData
    }
}