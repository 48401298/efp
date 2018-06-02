
//打开窗口
function openAppWindow1(title, url, width, height) {
    document.getElementById("appWindow1_Frm").src = url;
    var left = (document.body.offsetWidth - width) / 2;
    var top = (document.body.offsetHeight - height) / 2;
    $('#appWindow1').window({
        title: title,
        left: left,
        top: 10,
        width: width,
        modal: true,
        shadow: false,
        closed: false,
        closable: true,
        minimizable: false,
        maximizable: false,
        height: height,
        onClose: function () { document.getElementById("appWindow1_Frm").src = ""; },
        onMove: function (left, top) {
            var parentObj = $(this).window('window').parent();
            if (left > 0 && top > 0) {
                return;
            }
            if (left < 0) {
                $(this).window('move', {
                    left: 1
                });
            }
            if (top < 0) {
                $(this).window('move', {
                    top: 1
                });
            }
            var width = $(this).panel('options').width;
            var height = $(this).panel('options').height;
            var right = left + width;
            var buttom = top + height;
            var parentWidth = parentObj.width();
            var parentHeight = parentObj.height();
            if (parentObj.css("overflow") == "hidden") {
                if (left > parentWidth - width) {
                    $(this).window('move', {
                        "left": parentWidth - width
                    });
                }
                if (top > parentHeight - $(this).parent().height()) {
                    $(this).window('move', {
                        "top": parentHeight - $(this).parent().height()
                    });
                }
            }
        }
    });
    $('#appWindow1').window('open');

}

//关闭窗口
function closeAppWindow1() {
    $('#appWindow1').window('close');
}

//消息提示框
function MSIFade(title, message) {
    $.messager.show({
        title: title,
        msg: message,
        showType: 'fade'
    });
}

function MS(title, message) {
    $.messager.alert(title, message);
}
function MSA(title, message, OKfun) {
    $.messager.alert(title, message,"info", function (r) {
        OKfun();
    });
}
function MSE(title, message) {
    $.messager.alert(title, message, 'error');
}
function MSI(title, message) {
    //alert(message);
    $.messager.alert(title, message, 'info');
}

function MSW(title, message) {
    $.messager.alert(title, message, 'warning');
}

function MSQ(title, message, OKfun) {
    $.messager.confirm(title, message, function (r) {
        if (r) {
            OKfun();
        }
    });
}

function MSE(title, message, OKfun) {
    $.messager.prompt(title, message, function (r) {
        if (r) {
            OKfun(r);
        }
    });
}

//校验
function isValidate() {
    return $("#Mainform").form('validate');

}

//表格全选
function selectAll(cbSwitch) {
    var checkboxs = $(cbSwitch).parent().parent().parent().children("tr").children("td").children("input[type=checkbox]:enabled");
    if (!cbSwitch.checked) {
        checkboxs.removeAttr("checked");
    } else {
        checkboxs.prop("checked", true);
    }
}

//获取表格按钮脚本
function QLinkButtonHtml(buttonName, actionFunc) {
    var html = "";

    html = '<a href="#" onclick="' + actionFunc + '">' + buttonName + '</a>';

    return html;
}


//判断json集合项是否存在
function existsJsonItem(list, keyName, key) {
    var r = false;
    for (var i = 0; i < list.length; i++) {
        if (list[i][keyName] == key) {
            r = true;
            break;
        }
    }

    return r;
}

//查找json集合项
function findJsonItem(list, keyName, key) {
    var item = null;

    for (var i = 0; i < list.length; i++) {

        if (list[i][keyName] == key) {
            item = list[i];
            break;
        }
    }

    return item;
}

//删除json集合项
function deleteJsonItem(list, keyName, key) {
    for (var i = 0; i < list.length; i++) {
        if (list[i][keyName] == key) {
            list.splice(i, 1);
        }
    }

    return list;
}
