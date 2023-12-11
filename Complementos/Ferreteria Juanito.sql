BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Usuario";
CREATE TABLE IF NOT EXISTS "Usuario" (
	"UsuarioId"	INTEGER NOT NULL UNIQUE,
	"Nombre"	TEXT NOT NULL,
	"Correo"	TEXT NOT NULL,
	"Contraseña"	TEXT NOT NULL,
	"Rol"	INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("UsuarioId" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Producto";
CREATE TABLE IF NOT EXISTS "Producto" (
	"ProductoId"	INTEGER NOT NULL UNIQUE,
	"Nombre"	TEXT NOT NULL,
	"Precio"	NUMERIC NOT NULL DEFAULT 0,
	"UnidadesDeMedida"	INTEGER NOT NULL,
	"Stock"	INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("ProductoId" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Carrito";
CREATE TABLE IF NOT EXISTS "Carrito" (
	"CarritoId"	INTEGER NOT NULL UNIQUE,
	"UsuarioId"	INTEGER NOT NULL,
	"ProductoId"	INTEGER NOT NULL,
	"Cantidad"	INTEGER NOT NULL,
	PRIMARY KEY("CarritoId" AUTOINCREMENT),
	FOREIGN KEY("UsuarioId") REFERENCES "Usuario"("UsuarioId"),
	FOREIGN KEY("ProductoId") REFERENCES "Producto"("ProductoId")
);
DROP TABLE IF EXISTS "Ventas";
CREATE TABLE IF NOT EXISTS "Ventas" (
	"VentaId"	INTEGER NOT NULL UNIQUE,
	"UsuarioId"	INTEGER NOT NULL,
	"Fecha"	TEXT NOT NULL,
	PRIMARY KEY("VentaId" AUTOINCREMENT),
	FOREIGN KEY("UsuarioId") REFERENCES "Usuario"("UsuarioId")
);
DROP TABLE IF EXISTS "VentasDetalle";
CREATE TABLE IF NOT EXISTS "VentasDetalle" (
	"VentaDetalleId"	INTEGER NOT NULL UNIQUE,
	"VentaId"	INTEGER NOT NULL,
	"ProductoId"	INTEGER NOT NULL,
	"Cantidad"	INTEGER NOT NULL,
	"Precio"	NUMERIC NOT NULL,
	PRIMARY KEY("VentaDetalleId" AUTOINCREMENT),
	FOREIGN KEY("ProductoId") REFERENCES "Producto"("ProductoId"),
	FOREIGN KEY("VentaId") REFERENCES "Ventas"("VentaId")
);
INSERT INTO "Usuario" ("UsuarioId","Nombre","Correo","Contraseña","Rol") VALUES (1,'Alexis Calderon','acalderondiaz2@gmail.com','8c4fd8b2c24ffcc223dbf09088bd79734e8404cd4d9e90fc418ecb490622d1ca',0);
INSERT INTO "Usuario" ("UsuarioId","Nombre","Correo","Contraseña","Rol") VALUES (2,'Juanito Perez','juanitoperez@gmail.com','ed08c290d7e22f7bb324b15cbadce35b0b348564fd2d5f95752388d86d71bcca',1);
INSERT INTO "Producto" ("ProductoId","Nombre","Precio","UnidadesDeMedida","Stock") VALUES (1,'Alambre5',100.5,0,300);
INSERT INTO "Producto" ("ProductoId","Nombre","Precio","UnidadesDeMedida","Stock") VALUES (2,'Tornillos 1 pilgada',150,1,3500);
INSERT INTO "Producto" ("ProductoId","Nombre","Precio","UnidadesDeMedida","Stock") VALUES (3,'Tarugos 6 mm',180,1,2200);
INSERT INTO "Producto" ("ProductoId","Nombre","Precio","UnidadesDeMedida","Stock") VALUES (4,'Diluyente',550,2,80);
INSERT INTO "Producto" ("ProductoId","Nombre","Precio","UnidadesDeMedida","Stock") VALUES (5,'Alambre2',100,0,300);
INSERT INTO "Producto" ("ProductoId","Nombre","Precio","UnidadesDeMedida","Stock") VALUES (6,'Alambre22',100,0,0);
COMMIT;
