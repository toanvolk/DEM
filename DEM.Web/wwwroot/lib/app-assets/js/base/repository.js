//Chứa các xử lý tương tác object, server.
var objectJsonHandle = function () {
    //content: chứa các input
    //specifyMap: chỉ thị cụ thể mapping  giữa property và input name trong content. Type array [{property, inputname}]
    let _mappingObjectToInput = function (object, content, specifyMap, callback) {

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
    }
    let _mappingInputToObject = function (content, callback) {
        let _obj = {};

        let _dataArray = $(content).find(":input").not('input[ignore]').serializeArray();
        _dataArray.forEach(element => {
            _obj[element.name] = element.value
        });

        if (typeof (callback) == "function") callback(_obj);
        return _obj;
    }
    return {
        mappingObjectToInput: _mappingObjectToInput,
        mappingInputToObject: _mappingInputToObject
    }
}
