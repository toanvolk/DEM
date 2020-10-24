var expenseAddIndex = {
    dataSource: [],
    actionType: {
        MapDescriptionInput: 'map-expense',
        MapPayerInput: 'map-payer',
        AddData: 'add-data',
        SaveData: 'save-data',
        DeleteItem: 'delete-item',
        EditItem: 'edit-item',
        EnterMoney: 'enter-money',
        CategoryChange: "category-change"
    },
    initForm: function () {
        let _handle = _expenseAddHandle();
        expenseAddIndex.dataSource = [];
    },
    clickEvent: function (e, actionType) {
        let _handle = _expenseAddHandle();
        if (actionType == expenseAddIndex.actionType.AddData) expenseAddIndex.addData(e, _handle);
        if (actionType == expenseAddIndex.actionType.SaveData) expenseAddIndex.saveData(e, _handle);
        if (actionType == expenseAddIndex.actionType.DeleteItem) expenseAddIndex.deleteItem(e, _handle);
        if (actionType == expenseAddIndex.actionType.EditItem) expenseAddIndex.editItem(e, _handle);
        if (actionType == expenseAddIndex.actionType.CategoryChange) expenseAddIndex.categoryChange(e, _handle);
    },
    changeEvent: function (e, actionType) {
        let _handle = _expenseAddHandle();
        if (actionType == expenseAddIndex.actionType.MapDescriptionInput) expenseAddIndex.mapDescriptionInput(e, _handle);
        if (actionType == expenseAddIndex.actionType.MapPayerInput) expenseAddIndex.mapPayerInput(e, _handle);
    },
    keyupEvent: function (e, actionType) {
        let _handle = _expenseAddHandle();
        if (actionType == expenseAddIndex.actionType.EnterMoney) expenseAddIndex.enterMoney(e, _handle);
    },

    mapDescriptionInput: function (e, handle) {
        $(e).next().val($(e).val());
    },
    mapPayerInput: function (e, handle) {
        $(e).closest('.select-wrapper').next().val($(e).val());
    },

    addData: function (e, handle) {
        let _$container = $(e).closest('section#dem-expense-add');
        if (!handle.validate(_$container)) return;
        let _data = handle.data.inputToObject(_$container, function (obj) {
            let _dateString = obj.PayTime.split("/");
            obj.PayTime = new Date(_dateString[2] + '/' + _dateString[1] + '/' + _dateString[0]).toJSON();
            obj.Money = parseFloat(obj.Money.replaceAll(',', ''));

            let _selected = M.FormSelect.getInstance(_$container.find('select#Payer').last());
            obj.PayerName = _selected.input.value;
            obj.CategoryName = _$container.find('.dem-chips .chip.active').last().text().trim();

            let _objEdit = expenseAddIndex.dataSource.find(o => o.Id == obj.Id);
            if (_objEdit) {
                _objEdit.PayTime = obj.PayTime;
                _objEdit.PayerName = obj.PayerName;
                _objEdit.Payer = obj.Payer;
                _objEdit.Money = obj.Money;
                _objEdit.Description = obj.Description;
                _objEdit.CategoryId = obj.CategoryId;
                _objEdit.CategoryName = obj.CategoryName;
            }
            else {
                obj.Id = handle.newId();
                handle.addItem(expenseAddIndex.dataSource, obj);
            }
        });



        expenseAddIndex.createorRefreshTable();
        //clear input
        handle.data.clearInput({
            content: _$container,
            fieldExpel: ["Payer", "PayerName", "PayTime", "CategoryId"],
            callback: function (content) {
                content.find('textarea').val('');
            }
        });

        if (expenseAddIndex.dataSource.length > 0)
            _$container.find('button.dem-action-submit').last().show();
        else
            _$container.find('button.dem-action-submit').last().hide();
    },
    editItem: function (e, handle) {
        let _$container = $(e).closest('section#dem-expense-add');
        let _id = $(e).data("id");
        let _item = expenseAddIndex.dataSource.find(o => o.Id == _id);

        handle.data.objectToInput(
            object = _item,
            content = _$container,
            specifyMap = [
                {
                    inputName: "Id",
                    propertyName: "Id"
                },
                {
                    inputName: "Payer",
                    propertyName: "Payer"
                },
                {
                    inputName: "Description",
                    propertyName: "Description"
                },
                {
                    inputName: "Money",
                    propertyName: "Money"
                }
            ],
            callback = function (obj, content) {
                content.find('select#Payer').val(obj.Payer);
                //selected
                let _selected = M.FormSelect.getInstance(content.find('select#Payer').last());
                _selected._setValueToInput();

                content.find('textarea[name=Description]').val(obj.Description);

                //PayTime
                $(content.find('input[name=PayTime]')).val(moment(obj.PayTime).format('DD/MM/YYYY'));

                //change category
                content.find('.dem-chips .chip[data-id=' + obj.CategoryId + ']').last().click();
            }
        );
    },
    createorRefreshTable: function () {
        let _dataSource = new kendo.data.DataSource({
            data: expenseAddIndex.dataSource,
            aggregate: [
                { field: "Money", aggregate: "sum" }
            ],
            group: [
                {
                    field: "PayerName",
                    aggregates: [
                        { field: "Money", aggregate: "sum" }
                    ]
                }
            ],
        });
        let _grid = $('section#dem-expense-add .expense-grid').data('kendoGrid');
        if (_grid) {
            _grid.dataSource.read(_dataSource);
        }
        $('section#dem-expense-add .expense-grid').kendoGrid({
            columns: [
                {
                    field: "PayerName",
                    title: "Người ghi",
                    hidden: true,
                    groupHeaderTemplate: "Người thực hiện: #= value # - Tổng: #= kendo.toString(aggregates.Money.sum, '\\#\\#,\\#') #"
                },
                {
                    field: "PayTime",
                    title: 'Ngày',
                    template: "#= moment(PayTime).format('DD/MM/YYYY') #",
                    attributes: { class: "text-center" },
                    width: '12%'
                },
                {
                    field: "Description",
                    title: 'Mô tả',
                    width: '43%'
                },
                {
                    field: "CategoryName",
                    title: "Khoản",
                    width: "15%",
                },
                {
                    field: "Money", title: 'Số tiền',
                    template: "#= kendo.toString(Money, '\\#\\#,\\#') #",
                    footerTemplate: "<div class='text-right'>Tổng: #: kendo.toString(sum, '\\#\\#,\\#') # </div>",
                    attributes: { class: "text-right" },
                    width: "15%"
                },
                {
                    field: "",
                    width: '15%',
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-small btn-outline-danger round" onclick="expenseAddIndex.clickEvent(this, expenseAddIndex.actionType.DeleteItem)" data-id="' + item.Id + '"><i class="ft-trash-2"></i></button>';
                        _html += '<button type="button" class="btn btn-small btn-outline-info round" onclick="expenseAddIndex.clickEvent(this, expenseAddIndex.actionType.EditItem)" data-id="' + item.Id + '"><i class="ft-edit-3"></i></button>';
                        return _html;
                    },
                }

            ],
            groupable: true,
            groupExpand: function (e) {
                for (let i = 0; i < e.group.items.length; i++) {
                    var expanded = e.group.items[i].value
                    e.sender.expandGroup(".k-grouping-row:contains(" + expanded + ")");
                }
            },
            dataSource: _dataSource,

        });
    },

    saveData: function (e, handle) {
        handle.saveData(expenseAddIndex.dataSource, function (res) {
            if (res.statu == 200) {
                handle.closeDialog(
                    $(e).closest('section#dem-expense-add'), function () {
                        expenseAddIndex.dataSource = [];
                    }
                );
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
    deleteItem: function (e, handle) {
        let _id = $(e).data('id');
        expenseAddIndex.dataSource = handle.removeItem(expenseAddIndex.dataSource, _id);
        if (expenseAddIndex.dataSource.length == 0) $(e).closest('section#dem-expense-add').find('button.dem-action-submit').last().hide();
        expenseAddIndex.createorRefreshTable();
    },

    enterMoney: function (e, handle) {

        if (event.key === 'Enter' || event.keyCode === 13) {
            // Do something
            let _$container = $(e).closest('section#dem-expense-add');
            $(_$container).find('button.dem-action-info').click();

            $(_$container).find('textarea[name=Description]').focus();
        }
    },
    categoryChange: function (e, handle) {
        let _$demChips = $(e).closest('.dem-chips');
        _$demChips.find('.chip').removeClass('active');
        $(e).addClass('active');

        //set value
        let _categoryId = $(e).data('id');
        let _$container = $(e).closest('section#dem-expense-add');
        _$container.find('input[name=CategoryId]').last().val(_categoryId);
    }
}
let _expenseAddHandle = function () {
    let _addItem = function (source, obj) {
        source.push(obj);
    }
    let _removeItem = function (source, id) {
        source = $.grep(source, function (e) {
            return e.Id != id;
        });
        return source;
    }
    let _saveData = function (data, callback) {
        let _url = 'expense/create';
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }
    return {
        data: helper.formData,
        newId: helper.createGUID,
        addItem: _addItem,
        removeItem: _removeItem,
        saveData: _saveData,
        closeDialog: helper.closeDialog,
        validate: helper.inputValidate.checkRequired
    }
}