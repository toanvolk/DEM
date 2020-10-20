var intendedAdd = {
    dom: {
        demIntendedAdd: "section#dem-intended-add"
    },
    actionType: {
        MapDescription: "map-description",
        Save: "save",
        EnterMoney: "enter-money"
    },
    clickEvent: function (e, actionType) {
        let _handle = _intendedAddHandle();
        let _$rootDom = $(e).closest(intendedAdd.dom.demIntendedAdd);

        if (actionType == intendedAdd.actionType.Save) intendedAdd.save(e, _handle, _$rootDom);
    },
    changeEvent: function (e, actionType) {
        let _handle = _intendedAddHandle();
        let _$rootDom = $(e).closest(intendedAdd.dom.demIntendedAdd);

        if (actionType == intendedAdd.actionType.MapDescription) intendedAdd.mapDescription(e, _handle, _$rootDom);
    },
    keyupEvent: function (e, actionType) {
        let _handle = _intendedAddHandle();
        let _$rootDom = $(e).closest(intendedAdd.dom.demIntendedAdd);

        if (actionType == intendedAdd.actionType.EnterMoney) intendedAdd.enterMoney(e, _handle, _$rootDom);
    },
    mapDescription: function (e, handle, rootContent) {
        $(e).next().val($(e).val());
    },
    save: function (e, handle, rootContent) {        
        var _data = handle.data.inputToObject(rootContent, function (obj) {
            //generic Id
            obj.Id = handle.newId();

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
        let _$rootDom = $(e).closest(intendedAdd.dom.demIntendedAdd);
        let _total = 0;
        $.each(_$rootDom.find('.dem-intended-categorys input[name=Money]'), function (index, item) {
            _total += parseFloat(item.value ? item.value.replaceAll(',', '') : '0');
        });
        _$rootDom.find('.dem-intended-category-total input[name=Money]').last().val(_total);
    }
}
let _intendedAddHandle = function () {
    let _save = function (data, callback) {
        let _url = "intended/create"
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }

    return {
        data: helper.formData,
        newId: helper.createGUID,
        saveData: _save,
        closeDialog: helper.closeDialog
    }
}