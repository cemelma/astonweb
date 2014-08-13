$(function () {
      $("#btnsave").click(function () {
          CKEDITOR.instances.txtcontent.updateElement();
          $("form").validate();
    });
});