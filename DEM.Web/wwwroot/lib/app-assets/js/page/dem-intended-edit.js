var intendedEdit = {
    dom: {
        demintendedEdit: "section#dem-intended-edit"
    },
    actionType: {
        EnterMoney: "enter-money",
        Save: "save",
        MapDescription: "map-description",
    },
    clickEvent: function (e, actionType) {
        let _handle = _intendedEditHandle();
        let _$rootDom = $(e).closest(intendedEdit.dom.demintendedEdit);

        if (actionType == intendedEdit.actionType.Save) intendedEdit.save(e, _handle, _$rootDom);
    },
    changeEvent: function (e, actionType) {
        let _handle = _intendedEditHandle();
        let _$rootDom = $(e).closest(intendedEdit.dom.demintendedEdit);

        if (actionType == intendedEdit.actionType.MapDescription) intendedEdit.mapDescription(e, _handle, _$rootDom);
    },
    keyupEvent: function (e, actionType) {
        let _handle = _intendedEditHandle();
        let _$rootDom = $(e).closest(intendedEdit.dom.demintendedEdit);

        if (actionType == intendedEdit.actionType.EnterMoney) intendedEdit.enterMoney(e, _handle, _$rootDom);
    },
    //-----------------------------------------------------
    mapDescription: function (e, handle, rootContent) {
        $(e).next().val($(e).val());
    },
    save: function (e, handle, rootContent) {
        var _data = handle.data.inputToObject(rootContent, function (obj) {          
            let _$rangTime = rootContent.find('input[name=RangeTime]').last();
            obj.FromDate = _$rangTime.data('daterangepicker').startDate._d.toJSON();
            obj.ToDate = _$rangTime.data('daterangepicker').endDate._d.toJSON();
            obj.Details = [];
            let _$intendedDetails = rootContent.find('.dem-intended-categorys input[name=Money]');
            $(_$intendedDetails).each(function (index, item) {
                let _detail = {
                    IntendedId: obj.Id,
                    CategoryId: $(item).data('id'),
                    Money: parseFloat(item.value ? item.value.replaceAll(',', '') : '0')
                }
                obj.Details.push(_detail);
            });
        });

        //clear property not use
        delete _data.Money;
        delete _data.RangeTime;

        console.log(_data);
        handle.saveData(_data, function (res) {
            if (res.statu == 200) {
                handle.closeDialog(rootContent);
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
    enterMoney: function (e, handle) {
        let _$rootDom = $(e).closest(intendedEdit.dom.demintendedEdit);
        let _total = 0;
        $.each(_$rootDom.find('.dem-intended-categorys input[name=Money]'), function (index, item) {
            _total += parseFloat(item.value ? item.value.replaceAll(',', '') : '0');
        });
        _$rootDom.find('.dem-intended-category-total input[name=Money]').last().val(_total);
    }
}
let _intendedEditHandle = function () {
    let _save = function (data, callback) {
        let _url = "intended/update"
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }

    return {
        data: helper.formData,
        saveData: _save,
        closeDialog: helper.closeDialog
    }
}