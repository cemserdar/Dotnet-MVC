$(function () {
    $("#tblDepartmanlar").dataTable(
        {
            "language": {
                "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
            }
        }

    );
    $("#tblDepartmanlar").on("click", ".btnDepartmanSil", function () {
        var btn = $(this);
        bootbox.confirm("Departnans Silmek istediginize emin misiniz?", function (result) {
            if (result) {
                var id = btn.data("id");

                $.ajax({
                    type: "GET",
                    url: "/Departman/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                    }
                });
            }
        })
    });
});
