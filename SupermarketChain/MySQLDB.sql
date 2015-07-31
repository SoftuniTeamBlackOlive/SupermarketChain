DROP DATABASE IF EXISTS `supermarketchain`;

CREATE DATABASE `supermarketchain`
CHARACTER SET utf8 COLLATE utf8_unicode_ci;

USE `supermarketchain`;

DROP TABLE IF EXISTS `vendors`;

CREATE TABLE `vendors` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` nvarchar(45) NOT NULL,
  PRIMARY KEY (`id`)
);

DROP TABLE IF EXISTS `products`;

CREATE TABLE `products` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `vendor_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_products_vendors`
  FOREIGN KEY (`vendor_id`) 
  REFERENCES `vendors` (`id`)
);

DROP TABLE IF EXISTS `expenses`;

CREATE TABLE `expenses` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sum` decimal(10,2) NOT NULL,
  `period` datetime NOT NULL,
  `vendor_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_expenses_vendors`
  FOREIGN KEY (`vendor_id`)
  REFERENCES `vendors` (`id`)
);

CREATE TABLE `incomes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sum` decimal(10,2) NOT NULL,
  `product_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_incomes_product`
  FOREIGN KEY (`product_id`) 
  REFERENCES `products` (`id`)
);

