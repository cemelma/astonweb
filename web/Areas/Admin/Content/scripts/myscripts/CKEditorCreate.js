/// <reference path="../../CKEditor/ckeditor/ckeditor.js" />

$(function () {
    CKEDITOR.replace('txtcontent', {
        filebrowserBrowseUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.html"),
        filebrowserImageBrowseUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.html?type=Images"),
        filebrowserFlashBrowseUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.html?type=Flash"),
        filebrowserUploadUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files"),
        filebrowserImageUploadUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images"),
        filebrowserFlashUploadUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash"),
        height: 350
    });
    if ($("#txtcontent2").length > 0) {
        CKEDITOR.replace('txtcontent2', {
            filebrowserBrowseUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.html"),
            filebrowserImageBrowseUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.html?type=Images"),
            filebrowserFlashBrowseUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.html?type=Flash"),
            filebrowserUploadUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files"),
            filebrowserImageUploadUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images"),
            filebrowserFlashUploadUrl: ("/Areas/Admin/Content/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash"),
            height: 350
        });
    }
});


