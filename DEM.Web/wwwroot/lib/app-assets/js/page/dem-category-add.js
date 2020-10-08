var categoryAddIndex = {
    actionType: {
        Save: "save"
    },
    clickEvent: function (e, type) {
        let _handle = categoryAddHandle();
        if (type == categoryAddIndex.actionType.Save) categoryAddIndex.saveData(e, _handle);
    },

    //children event
    saveData: function (e, handle) {

    }
}
var categoryAddHandle = function () {

}