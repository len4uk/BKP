CREATE USER 'IBS'@'%' IDENTIFIED BY 'IBS';
CREATE USER 'IBS'@'localhost' IDENTIFIED BY 'IBS';

GRANT USAGE ON *.* TO 'IBS'@'%';
GRANT USAGE ON *.* TO 'IBS'@'localhost';

GRANT ALL PRIVILEGES ON IBS.* to 'IBS'@'%';
GRANT ALL PRIVILEGES ON IBS.* to 'IBS'@'localhost';

CREATE DATABASE IF NOT EXISTS IBS;

USE IBS;

-- MySQL dump 10.13  Distrib 5.6.11, for Win32 (x86)
--
-- Host: localhost    Database: atlant_beton
-- ------------------------------------------------------
-- Server version	5.6.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `login` varchar(20) UNIQUE NOT NULL COMMENT 'логин',
  `password` varchar(20) NOT NULL COMMENT 'пароль',
  `fio` varchar(255) NOT NULL COMMENT 'ФИО',
  `phone` varchar(20) UNIQUE NULL  COMMENT 'Телефон',
  `email` varchar(50) UNIQUE NULL  COMMENT 'Почта',
  `number_card` varchar(50) UNIQUE NULL  COMMENT 'номер карты',
  PRIMARY KEY (`id`),
  KEY `FK_users_id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `stop_kazan`
--

DROP TABLE IF EXISTS `stop_kazan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stop_kazan` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `name` varchar(255) NOT NULL COMMENT 'название остановки',
  `latitude` DOUBLE NOT NULL COMMENT 'широта',
  `longitude` DOUBLE NOT NULL COMMENT 'долгота',
  PRIMARY KEY (`id`),
  KEY `FK_users_id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stop_kazan`
--

LOCK TABLES `stop_kazan` WRITE;
/*!40000 ALTER TABLE `stop_kazan` DISABLE KEYS */;
INSERT INTO `stop_kazan` VALUES 
(1,'Горьковское шоссе',55.826407, 49.043037),
(2,'ПО Тасма',55.831495, 49.051767),
(3,'Разъезд Восстания',55.832925, 49.062844),
(4,'ул. Исаева',55.833054, 49.068065),
(5,'Социально-юридический институт',55.833120, 49.071506),
(6,'ул. Восход',55.833216, 49.075325),
(7,'пл. Восстания',55.833216, 49.075325),
(8,'ул. Восстания',55.834740, 49.090646),
(9,'Кафе "Солнышко"',55.835692, 49.099916),
(10,'ул. Голубятникова',55.835820, 49.105559),
(11,'ж/м Дружба',55.835850, 49.113005),
(12,'ул. Маршала Чуйкова',55.835790, 49.122439),
(13,'39-й Квартал',55.835802, 49.130056),
(14,'7-я Поликлиника',55.835814, 49.136901),
(15,'Роддом',55.835790, 49.147927),
(16,'ул. Академика Лаврентьева',55.835784, 49.151618),
(17,'ул. Гаврилова',55.835784, 49.151618),
(18,'Ак Барс Арена',55.823499, 49.164298),
(19,'Ветеринарная академия',55.818768, 49.176464),
(20,'ул. Шамиля Усманова',55.831926, 49.065258);
/*!40000 ALTER TABLE `stop_kazan` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `route_kazan`
--

DROP TABLE IF EXISTS `route_kazan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `route_kazan` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `number` varchar(10) NOT NULL COMMENT 'номер маршрута',
  `list_bus` TEXT NOT NULL COMMENT 'список автобусов',
  `is_circle` TINYINT(1) NOT NULL COMMENT 'Круговой маршрут',
  `type` enum('None','Bus','Trolleybus','Tram') NOT NULL DEFAULT 'None',
  `direction` INT(2) NOT NULL COMMENT 'Направление',
  `average_workload`int(5) NOT NULL COMMENT 'средняя загруженость',
  PRIMARY KEY (`id`),
  KEY `FK_users_id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `route_kazan`
--

LOCK TABLES `route_kazan` WRITE;
/*!40000 ALTER TABLE `route_kazan` DISABLE KEYS */;
INSERT INTO `route_kazan` VALUES 
(1,3,'1',0, 'Bus',1,50),
(2,13,'2',0, 'Trolleybus',1,100),
(3,5,'3',0, 'Tram',1,20);
/*!40000 ALTER TABLE `route_kazan` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `route_3_bus_stop`
--

DROP TABLE IF EXISTS `route_3_bus_stop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `route_kazan_3_bus_stop` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `id_stop` int(11) NOT NULL COMMENT 'id остановки',
  `minutes_1` int(11) NOT NULL COMMENT 'время до следующей остановки прямое направление',
  `minutes_2` int(11) NOT NULL COMMENT 'время до следующей остановки обратное направление',
  `id_transport` int(11) NOT NULL DEFAULT 0 COMMENT 'id транспортного средаства'
  PRIMARY KEY (`id`),
  KEY `FK_users_id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `route_3_bus_stop`
--

LOCK TABLES `route_3_bus_stop` WRITE;
/*!40000 ALTER TABLE `route_3_bus_stop` DISABLE KEYS */;
INSERT INTO `route_3_bus_stop` VALUES 
(1,1,5,0),
(2,3,10,5),
(3,6,3,10),
(4,9,7,3),
(5,20,0,7);
/*!40000 ALTER TABLE `route_3_bus_stop` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `transport_kazan_bus`
--

DROP TABLE IF EXISTS `transport_kazan_bus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `transport_kazan_bus` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `is_route` TINYINT(1) NOT NULL COMMENT 'на маршруте',
  `direction` TINYINT(1) NOT NULL DEFAULT 0 COMMENT 'направление',
  `load` int(3) NOT NULL DEFAULT 0  COMMENT 'загруженность',
  PRIMARY KEY (`id`),
  KEY `FK_users_id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transport_kazan_bus`
--

LOCK TABLES `transport_kazan_bus` WRITE;
/*!40000 ALTER TABLE `transport_kazan_bus` DISABLE KEYS */;
INSERT INTO `transport_kazan_bus` VALUES 
(1,1,1),
(2,0,1);
/*!40000 ALTER TABLE `transport_kazan_bus` ENABLE KEYS */;
UNLOCK TABLES;

UPDATE mysql.user SET password=PASSWORD('1q2w3e4r5T') WHERE user='root';

flush privileges;