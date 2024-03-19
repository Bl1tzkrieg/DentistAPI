-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         8.0.34 - MySQL Community Server - GPL
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


/* Creacion de la base de datos */
CREATE DATABASE IF NOT EXISTS odontologia;

/* seleccionar la base de datos para la creacion de tablas */
USE odontologia;

-- Volcando estructura para tabla odontologia.clientes
DROP TABLE IF EXISTS `clientes`;
CREATE TABLE IF NOT EXISTS `clientes` (
  `Cli_Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Cli_Cedula` varchar(8) NOT NULL DEFAULT '',
  `Cli_Nombre1` varchar(50) NOT NULL DEFAULT '',
  `Cli_Nombre2` varchar(50) DEFAULT '',
  `Cli_Apellido1` varchar(50) NOT NULL DEFAULT '',
  `Cli_Apellido2` varchar(50) DEFAULT '',
  `Cli_Status` bit(1) DEFAULT (1),
  `Cli_Telefono` varchar(11) NOT NULL DEFAULT 'NO ASIGNADO',
  PRIMARY KEY (`Cli_Id`),
  UNIQUE KEY `Cli_Cedula` (`Cli_Cedula`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.compras
DROP TABLE IF EXISTS `compras`;
CREATE TABLE IF NOT EXISTS `compras` (
  `Comp_Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Comp_Usu_Id` smallint unsigned NOT NULL DEFAULT (0),
  `Comp_Fecha` date NOT NULL DEFAULT '1000-01-01',
  `Comp_Ped_Id` int NOT NULL DEFAULT (0),
  `Comp_NroFactura` varchar(200) NOT NULL DEFAULT '',
  `Comp_Total` decimal(10,2) NOT NULL DEFAULT (0),
  PRIMARY KEY (`Comp_Id`),
  KEY `FK1_Compras_Usu` (`Comp_Usu_Id`),
  KEY `FK2_Compras_Ped` (`Comp_Ped_Id`),
  CONSTRAINT `FK1_Compras_Usu` FOREIGN KEY (`Comp_Usu_Id`) REFERENCES `usuarios` (`Usu_Id`),
  CONSTRAINT `FK2_Compras_Ped` FOREIGN KEY (`Comp_Ped_Id`) REFERENCES `pedido_insumo` (`Ped_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.consultas
DROP TABLE IF EXISTS `consultas`;
CREATE TABLE IF NOT EXISTS `consultas` (
  `Consult_Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Consult_Id_Cliente` int unsigned NOT NULL DEFAULT (0),
  `Consult_Descripcion` varchar(300) NOT NULL DEFAULT '',
  `Consult_Usu_Id` smallint unsigned NOT NULL DEFAULT '0',
  `Consult_FecAsig` date NOT NULL DEFAULT '1000-01-01',
  `Consult_FecGen` date NOT NULL DEFAULT '1000-01-01',
  `Consult_Status` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'PENDIENTE',
  `Consult_Facturada` varchar(50) NOT NULL DEFAULT 'POR FACTURAR',
  PRIMARY KEY (`Consult_Id`),
  KEY `FK1_Consult_Usu` (`Consult_Usu_Id`),
  KEY `FK2_Consult_Cli` (`Consult_Id_Cliente`),
  CONSTRAINT `FK1_Consult_Usu` FOREIGN KEY (`Consult_Usu_Id`) REFERENCES `usuarios` (`Usu_Id`),
  CONSTRAINT `FK2_Consult_Cli` FOREIGN KEY (`Consult_Id_Cliente`) REFERENCES `clientes` (`Cli_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.empleados
DROP TABLE IF EXISTS `empleados`;
CREATE TABLE IF NOT EXISTS `empleados` (
  `Emp_Id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Emp_Cedula` varchar(50) NOT NULL DEFAULT '',
  `Emp_Nombre1` varchar(50) NOT NULL DEFAULT '',
  `Emp_Nombre2` varchar(50) DEFAULT '',
  `Emp_Apellido1` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Emp_Apellido2` varchar(50) DEFAULT '',
  `Emp_Correo` varchar(200) NOT NULL DEFAULT '',
  PRIMARY KEY (`Emp_Id`),
  UNIQUE KEY `Emp_Cedula` (`Emp_Cedula`),
  UNIQUE KEY `Emp_Correo` (`Emp_Correo`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.pedido_insumo
DROP TABLE IF EXISTS `pedido_insumo`;
CREATE TABLE IF NOT EXISTS `pedido_insumo` (
  `Ped_Id` int NOT NULL AUTO_INCREMENT,
  `Ped_Cant` smallint unsigned NOT NULL DEFAULT (0),
  `Ped_Descripcion` varchar(300) NOT NULL DEFAULT '',
  `Ped_Usu_Id` smallint unsigned NOT NULL DEFAULT '0',
  `Ped_Fecha` date NOT NULL DEFAULT '1000-01-01',
  `Ped_Status` varchar(50) NOT NULL DEFAULT 'PENDIENTE',
  PRIMARY KEY (`Ped_Id`),
  KEY `FK1_Ped_Usu` (`Ped_Usu_Id`),
  CONSTRAINT `FK1_Ped_Usu` FOREIGN KEY (`Ped_Usu_Id`) REFERENCES `usuarios` (`Usu_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.roles
DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `Rol_Id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Rol_Descripcion` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`Rol_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.tipo_pago
DROP TABLE IF EXISTS `tipo_pago`;
CREATE TABLE IF NOT EXISTS `tipo_pago` (
  `Tp_Id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Tp_Descripcion` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Tp_Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.usuarios
DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `Usu_Id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Usu_UserName` varchar(100) NOT NULL DEFAULT '',
  `Usu_Password` varchar(100) NOT NULL DEFAULT '',
  `Usu_Rol_Id` tinyint unsigned NOT NULL DEFAULT (0),
  `Usu_Emp_Id` smallint unsigned NOT NULL DEFAULT (0),
  `Usu_Status` bit(1) NOT NULL DEFAULT (1),
  PRIMARY KEY (`Usu_Id`),
  UNIQUE KEY `Usu_UserName` (`Usu_UserName`),
  KEY `FK1_Usu_Emp` (`Usu_Emp_Id`),
  KEY `FK2_Usu_Rol` (`Usu_Rol_Id`),
  CONSTRAINT `FK1_Usu_Emp` FOREIGN KEY (`Usu_Emp_Id`) REFERENCES `empleados` (`Emp_Id`),
  CONSTRAINT `FK2_Usu_Rol` FOREIGN KEY (`Usu_Rol_Id`) REFERENCES `roles` (`Rol_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.ventas
DROP TABLE IF EXISTS `ventas`;
CREATE TABLE IF NOT EXISTS `ventas` (
  `Venta_Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Venta_Cli_Id` int unsigned NOT NULL DEFAULT (0),
  `Venta_FecGen` date NOT NULL DEFAULT '1000-01-01',
  `Venta_NroFactura` varchar(200) NOT NULL DEFAULT '',
  `Venta_Usu_Id` smallint unsigned NOT NULL DEFAULT (0),
  `Venta_Vd_Id` int unsigned NOT NULL DEFAULT '0',
  `Venta_Consult_Id` int NOT NULL,
  PRIMARY KEY (`Venta_Id`),
  UNIQUE KEY `Venta_NroFactura` (`Venta_NroFactura`),
  KEY `FK1_Venta_Cli` (`Venta_Cli_Id`),
  KEY `FK2_Venta_Usu` (`Venta_Usu_Id`),
  KEY `FK3_Venta_Vd` (`Venta_Vd_Id`),
  CONSTRAINT `FK1_Venta_Cli` FOREIGN KEY (`Venta_Cli_Id`) REFERENCES `clientes` (`Cli_Id`),
  CONSTRAINT `FK2_Venta_Usu` FOREIGN KEY (`Venta_Usu_Id`) REFERENCES `usuarios` (`Usu_Id`),
  CONSTRAINT `FK3_Venta_Vd` FOREIGN KEY (`Venta_Vd_Id`) REFERENCES `ventas_detalle` (`Vd_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla odontologia.ventas_detalle
DROP TABLE IF EXISTS `ventas_detalle`;
CREATE TABLE IF NOT EXISTS `ventas_detalle` (
  `Vd_Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Vd_TipoDePago` tinyint unsigned NOT NULL DEFAULT '0',
  `Vd_Total` decimal(10,2) unsigned NOT NULL DEFAULT (0),
  PRIMARY KEY (`Vd_Id`),
  KEY `FK1_Vd_TipoDePago` (`Vd_TipoDePago`) USING BTREE,
  CONSTRAINT `FK1_tipo_pago` FOREIGN KEY (`Vd_TipoDePago`) REFERENCES `tipo_pago` (`Tp_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- La exportación de datos fue deseleccionada.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
