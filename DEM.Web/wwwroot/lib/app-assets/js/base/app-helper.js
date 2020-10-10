var helper = {
    createGUID: function() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },
    colorRandom: function () {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    },
    showDialog: function (obj) {
        let _url = "/kwindow";
        let _data = {
            Name :"k-window-custom",
            Title : "Title-window",
            Draggable : true,
            Resizable : false,
            Width: 600,
            Actions: [
                //"Pin",
                //"Minimize",
                "Maximize",
                "Close"
            ],
            ActivateEvent: undefined,
            OpenEvent: undefined,
            CloseEvent: undefined,
            RefreshEvent: undefined,
            ActionName: "",           
            ControllerName:""
        }
        if (obj) {
            _data.Name = obj.Name ?? _data.Name;
            _data.Title = obj.Title ?? _data.Title;
            _data.Draggable = obj.Draggable ?? _data.Draggable;
            _data.Resizable = obj.Resizable ?? _data.Resizable;
            _data.Width = obj.Width ?? _data.Width;
            _data.Actions = obj.Actions ?? _data.Actions;
            _data.ActivateEvent = obj.ActivateEvent ?? _data.ActivateEvent;
            _data.OpenEvent = obj.OpenEvent ?? _data.OpenEvent;
            _data.CloseEvent = obj.CloseEvent ?? _data.CloseEvent;
            _data.RefreshEvent = obj.RefreshEvent ?? _data.RefreshEvent;
            _data.ActionName = obj.ActionName ?? _data.ActionName;
            _data.ControllerName = obj.ControllerName ?? _data.ControllerName;
        }
        if (!(_data.ActionName + _data.ControllerName)) return;
        $.get(_url, _data, function (res) {
            $('body').append(res);
            $(res).data("kendoWindow").center().open();
        });
    },
    inputValidate: {
        checkRequired: function (obj) {
            const VALIDATE_MESSAGE_REQUIRED = 'validate-message-required';
            let _valid = true;
            let _html = '<div class="invalid-feedback">{{message}}</div>';
            let _data = {
                content: undefined,
                takeIgnore: false //FALSE: KHÔNG duyệt dom có thuộc tính ignore. TRUE: duyệt dom có thuộc tính ignore.
            };
            if (obj) {
                //vùng được check input
                _data.content = obj.content || _data.content;
                //Cho phép check input có property IGNORE. Mặc định check = true
                _data.takeIgnore = obj.takeIgnore || _data.takeIgnore;
            }
            let _content;
            if (typeof (_data.content) == 'object') {
                if (!_data.takeIgnore)
                    _content = _data.content.find('input[required]:not([ignore])');
                else
                    _content = _data.content.find('input[required]');
            }
            else{
                _content = (_data.content ? _data.content + " " : "") + "input[required]";
                if (!_data.takeIgnore)
                    _content = _content + ':not([ignore])';
                _content = $(_content);
            }
            
            _content.each(function (i, dom) {
                if (!$(dom).val()) {
                    if (!$(dom).next().hasClass('invalid-feedback')) {
                        let _message = $(dom).attr(VALIDATE_MESSAGE_REQUIRED);
                        _message = _message || "Please enter..!";
                        let _regex = new RegExp("{{message}}", "gi");
                        _html = _html.replace(_regex, _message)
                        $(_html).insertAfter($(dom));
                    }
                    $(dom).next().show();
                    _valid = false;
                    return;

                }
                else {
                    if ($(dom).next().hasClass('invalid-feedback')) {
                        $(dom).next().hide();
                    }
                }
            });
            return _valid;
        }
    },
    formData: {
        inputToObject: function (content, callback) {
            let _obj = {};

            let _dataArray = $(content).find(":input").not('input[ignore]').serializeArray();
            _dataArray.forEach(element => {
                _obj[element.name] = element.value
            });

            if (typeof (callback) == "function") callback(_obj);
            return _obj;
        },
        objectToInput: function (object, content, specifyMap, callback) {
            if (typeof (specifyMap) == "undefined") {
                $.each(object, function (key, value) {
                    content.find("Input[name=" + key + "]")?.val(value);
                });
            }
            else {
                if (Array.isArray(specifyMap)) {
                    specifyMap.forEach(element => {
                        content.find("input[name=" + element.inputName + "]")?.val(object[element.property])
                    });
                }
            }
            if (typeof (callback) == "function") callback(object, content);
        },
        clearInput: function (obj) {
            let _data = {
                content: "",
                callback: undefined
            };
            if (obj) {
                _data.content = obj.content ?? _data.content;
                _data.callback = obj.callback ?? _data.callback;
            }
            if (typeof (_data.content) == 'object') {
                _data.content.find('input').val('');
            }
            else {
                $(_data.content).find('input').val('');
            }
            if (_data.callback) _data.callback(_data.content);
        }
    }
}