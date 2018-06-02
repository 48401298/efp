//打开页面
function openUrl(menuID, url, target, title, closable) {    
    if (target == "main") {
        if (title != null && $("#MenuTitle") != null)
            $("#MenuTitle").panel({ title: title });
        document.getElementById("frmMain").src = url;
    }
    else if (target == "tab" || target == "url") {
        var tt = $('#divTab');
        if (tt.tabs('exists', title)) {
            tt.tabs('select', title);
            tt.tabs('update', {
                tab: tt.tabs('getSelected'),
                options: tt.tabs('options')
            });
        } else {
            var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;"></iframe>';
            tt.tabs('add', {
                title: title,
                content: content,
                closable: (closable == undefined) ? true : closable//默认是带关闭
            });
        }
    }
}
