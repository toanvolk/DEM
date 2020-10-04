(function (jQuery) {
    var toastIndex = {
        show: function (obj) {
            let _toast = _toastHandle(obj);
            _toast.addContent(function () { _toast.show() });
        }
    }
    let _toastHandle = function (obj) {
        let _url = "/lib/app-assets/html/base/toast.html";
        let _$content = $('body');
        let _toastDom = "section#bootstrap-toasts";
        let _toastDomAction = '.toast-bs-container .toast-position .toast';
        let _toastHeaderClass = '.toast-header';

        let _data = {
            title: "Unknown title",
            description: "Unknown content",
            addClassHeader: ''
        }

        if (obj) {
            _$content = obj.content || _$content;
            _data.title = obj.title || _data.title;
            _data.description = obj.description || _data.description;
            _data.addClassHeader = obj.addClassHeader || _data.addClassHeader;
        }

        let _checkToastExists = function () {
            if (_$content.find(_toastDom).length == 0) {
                return false;
            }
            return true;
        }
        let _addContent = function (callback) {
            $.get(_url, function (res) {
                $.each(_data, function (key, value) {
                    let _regex = new RegExp("{{" + key + "}}", "gi");
                    res = res.replace(_regex, value);
                });
                if (!_checkToastExists())
                    _$content.append(res);
                else
                    _$content.find(_toastDom).append($(res).html());
                callback();
            });
        }
        let _show = function () {
            if ($(_toastDomAction).hasClass('hide')) {
                $(_toastDomAction).toast('show');
            }
        }

        return {
            show: _show,
            checkToastExists: _checkToastExists,
            addContent: _addContent
        }
    }

    //set jQuery
    jQuery.prototype.toastIndex = toastIndex;
})(jQuery);
