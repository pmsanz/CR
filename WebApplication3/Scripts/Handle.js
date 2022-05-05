
var productosArray = [];

function CalcularTotal() {



    var a = $('.subtotal').not('.grid-header');
    console.log("valor de a :" + a[0].innerText);
    var total = parseFloat(0);

    for (var i = 0; i < a.length; i++) {

        console.log(parseFloat(a[i].innerText.replace("$", "")))
        total += parseFloat(a[i].innerText.replace("$", ""));

    }
    console.log("valor de total: " + total);
    $('#txtTotales').val(total);

}


function QuitarProducto(id) {

    console.log("Se quitara el producto ID : " + id);

    for (var i = 0; i < productosArray.length; i++) {
        if (productosArray[i].productoID == id) {
            productosArray.pop(productosArray[i]);
        }
    }

    console.log(JSON.stringify(productosArray));


    $.ajax({
        type: "POST",
        url: '@Url.Action("AgregarProductoGrid")',
        data: JSON.stringify(productosArray),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // alert("Data has been added successfully.");
            $('#GridProducto').html(data);
            //$("#StudentName").removeAttr("placeholder");
            // $("#buscarCliente").val(data.data);
            console.log("Respuesta:" + data);
        },
        error: function () {
            console.log("Error al retornar");
            // alert("Error while inserting data");
        }
    });
    return false;


}


    $("#btnSearchCliente").click(function () {

        console.log("dentro de function cliente");
        //console.log('{keyword: ' + JSON.stringify($("#buscarCliente").val()) + '}');
        $.ajax({
            type: "POST",
            url: '@Url.Action("BuscarNombreCliente")',
            data: '{keyword: ' + JSON.stringify($("#buscarCliente").val()) + '}',
            dataType: "html",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                // alert("Data has been added successfully.");

                //$("#StudentName").removeAttr("placeholder");
                // $("#buscarCliente").val(data.data);
                $('#ClienteDetalle').html(data);
            },
            error: function () {
                console.log("Error al retornar");
                // alert("Error while inserting data");
            }
        });
        return false;

    });

    $("#btnSearchProducto").click(function () {

        console.log("dentro de function prodcuto");

        $.ajax({
            type: "POST",
            url: '@Url.Action("BuscarProducto")',
            data: '{keyword: ' + JSON.stringify($("#buscarProducto").val()) + '}',
            dataType: "html",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                // alert("Data has been added successfully.");

                //$("#StudentName").removeAttr("placeholder");
                // $("#buscarCliente").val(data.data);
                $('#ProductoDetalle').html(data);
            },
            error: function () {
                console.log("Error al retornar");
                // alert("Error while inserting data");
            }
        });
        return false;

    });


    $("#btnAgregarProducto").click(function () {

        console.log("dentro de function cliente: " + $("#ProductoID").val());


        var flag = 0;

        for (var i = 0; i < productosArray.length; i++) {
            if (productosArray[i].productoID == $("#ProductoID").val()) {
                productosArray[i].cantidad += parseInt($("#txtCantidad").val());
                productosArray[i].subtotal = parseFloat(productosArray[i].cantidad * parseFloat($("#txtPrecio").val()));
                flag = 1;
            }

        }
        console.log("Antes " + productosArray);

        if (flag == 0) {
            productosArray.push({
                productoID: $("#ProductoID").val(),
                descripcion: $("#txtDescripcion").val(),
                cantidad: parseInt($("#txtCantidad").val()),
                precio: parseFloat($("#txtPrecio").val()),
                subtotal: parseFloat(parseInt($("#txtCantidad").val()) * parseFloat($("#txtPrecio").val()))
            });
        }

        console.log("Despues " + productosArray);
        //var product = { productoID: $("#ProductoID").val(), cantidad: $("#txtCantidad").val() };

        // arrayProductos.push(product);

        //var keyword = JSON.stringify(arrayProductos);
        //var dat = JSON.stringify(arrayProductos);

        // console.log(JSON.stringify($("#buscarProducto").val()));
        console.log(JSON.stringify(productosArray));
        $.ajax({
            type: "POST",
            url: '@Url.Action("AgregarProductoGrid")',
            data: JSON.stringify(productosArray),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                // alert("Data has been added successfully.");
                $('#GridProducto').html(data);
                CalcularTotal();
                //$("#StudentName").removeAttr("placeholder");
                // $("#buscarCliente").val(data.data);
                console.log("Respuesta:" + data);
            },
            error: function () {
                console.log("Error al retornar");
                // alert("Error while inserting data");
            }
        });
        return false;




    });

