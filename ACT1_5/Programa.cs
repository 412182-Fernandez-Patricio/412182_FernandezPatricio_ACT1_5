using ACT1_5.Datos.Implementaciones;
using ACT1_5.Dominio;
using ACT1_5.Servicios;
using System.Data;
var articuloManager = new ArticuloManager(new ArticuloRepositoryADO());
var facturaManager = new FacturaManager(new FacturaRepositoryADO());
var tipoPagoManager = new TipoPagoManager(new TipoPagoRepositoryADO());

List<Articulo>? articulos = null;
List<Factura?>? facturas = null;
Console.WriteLine("\t\t ---ARTICULOS---\n");
//GetArticulos
MostrarArticulos();

//SaveArticulo
var art1 = new Articulo()
{
    Descripcion = "FILETE.PEJERREY"
};
Console.WriteLine("\tSaveArticulo\n");
if(articuloManager.SaveArticulo(art1))
{
    Console.WriteLine("El articulo '" + art1.Descripcion + "' fue guardado con exito\n");
}
else
{
    Console.WriteLine("El articulo '" + art1.Descripcion + "' no fue guardado con exito\n");
}

//GetArticulos
MostrarArticulos();

//DeleteArticulo

Console.WriteLine("\tDeleteArticulo (4)\n");
if (articuloManager.DeleteArticulo(4))
{
    Console.WriteLine("El articulo de id = 4 fue borrado con exito\n");
}
else
{
    Console.WriteLine("El articulo de id = 4 no fue borrado con exito\n");
}

//GetArticulos
MostrarArticulos();

//GetArticuloById
Console.WriteLine("\tGetArticuloById (2)\n");
var art2 = articuloManager.GetArticuloById(2);
if (art2 != null)
{
    Console.WriteLine("Articulo recuperado con exito!! \nARTICULO");
    Console.WriteLine(art2.Id.ToString() + " - " + art2.Descripcion);
}


//FACTURAS!!
Console.WriteLine("\t\t ---FACTURAS---\n");
//SaveFactura
Console.WriteLine("\tSaveFactura 1\n");
var tiposPagos = tipoPagoManager.GetTiposPagos();
articulos = null;
articulos = articuloManager.GetArticulos();

var det1_1 = new DetalleFactura(articulos[1], 23, (float)120.5);
var det1_2 = new DetalleFactura(articulos[2], 12, (float)43.25);
var detalles1 = new List<DetalleFactura>() { det1_1, det1_2};

Factura factura1 = new Factura(tiposPagos[0], detalles1, "RAUL");

if (facturaManager.SaveFactura(factura1))
{
    Console.WriteLine("Factura 1 guardada con exito\n");
}
else
{
    Console.WriteLine("Factura 1 no fue guardada con exito");
}

Console.WriteLine("\tSaveFactura 2\n");
var det2_1 = new DetalleFactura(articulos[0], 33, (float)120.5);
var det2_2 = new DetalleFactura(articulos[2], 3, (float)43.25);
var det2_3 = new DetalleFactura(articulos[2], 1, (float)76.99);
var detalles2 = new List<DetalleFactura>() { det2_1, det2_2, det2_3 };

Factura factura2 = new Factura(tiposPagos[1], detalles2, "JORGE");

if (facturaManager.SaveFactura(factura2))
{
    Console.WriteLine("Factura 2 guardada con exito\n");
}
else
{
    Console.WriteLine("Factura 2 no fue guardada con exito");
}

//GetFacturaByID NO FUNCIONA
Console.WriteLine("\tGetFacturaById (1)\n");
var facturaXD = facturaManager.GetFacturaById(1);
Console.WriteLine(facturaXD.NroFactura.ToString() + " - " + facturaXD.Fecha.ToString() + " - " + facturaXD.Cliente + " - " + facturaXD.TipoPago.Descripcion);
foreach (var detalle in facturaXD.GetDetallesFacturas())
{
    Console.WriteLine("-\t" + detalle.Id.ToString() + " - " + detalle.Articulo.Descripcion + " - C: " + detalle.Cantidad.ToString() + " - PU: " + detalle.PrecioUnidad.ToString());
}

//GetFacturas
MostrarFacturas();

void MostrarArticulos()
{
    articulos = null;
    articulos = articuloManager.GetArticulos();
    Console.WriteLine("\tGetArticulos\n");
    foreach (var articulo in articulos)
    {
        Console.WriteLine(articulo.Id.ToString() + " - " + articulo.Descripcion + "\n------------------------------------");
    }
}

void MostrarFacturas()
{
    facturas = null;
    facturas = facturaManager.GetFacturas();
    Console.WriteLine("\tGetFacturas\n");
    if(facturas != null)
    {
        foreach (var factura in facturas)
        {
            Console.WriteLine(factura.NroFactura.ToString() + " - " + factura.Fecha.ToString() + " - " + factura.Cliente + " - " + factura.TipoPago.Descripcion);
            foreach (var detalle in factura.GetDetallesFacturas())
            {
                Console.WriteLine("-\t" + detalle.Id.ToString() + " - " + detalle.Articulo.Descripcion + " - C: " + detalle.Cantidad.ToString() + " - PU: " + detalle.PrecioUnidad.ToString());
            }
            Console.WriteLine("\n----------------");
        }
    }
    else
    {
        Console.WriteLine("No hay facturas registradas");
    }
    

}


