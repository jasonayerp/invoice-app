CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    ALTER DATABASE `invoice` CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE TABLE `addresses` (
        `address_id` BIGINT NOT NULL AUTO_INCREMENT,
        `guid` VARCHAR(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT (UUID()),
        `address_line_1` VARCHAR(128) CHARACTER SET utf8mb4 NOT NULL,
        `address_line_2` VARCHAR(128) CHARACTER SET utf8mb4 NULL DEFAULT null,
        `address_line_3` VARCHAR(128) CHARACTER SET utf8mb4 NULL DEFAULT null,
        `address_line_4` VARCHAR(128) CHARACTER SET utf8mb4 NULL DEFAULT null,
        `city` VARCHAR(128) CHARACTER SET utf8mb4 NOT NULL,
        `region` VARCHAR(128) CHARACTER SET utf8mb4 NOT NULL,
        `postal_code` VARCHAR(128) CHARACTER SET utf8mb4 NOT NULL,
        `country_code` CHAR(2) CHARACTER SET utf8mb4 NOT NULL,
        `utc_created_date` DATETIME NOT NULL,
        `utc_updated_date` DATETIME NULL DEFAULT null,
        `utc_deleted_date` DATETIME NULL DEFAULT null,
        CONSTRAINT `pk_addresses` PRIMARY KEY (`address_id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE TABLE `clients` (
        `client_id` BIGINT NOT NULL AUTO_INCREMENT,
        `guid` VARCHAR(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT (UUID()),
        `name` VARCHAR(50) CHARACTER SET utf8mb4 NOT NULL,
        `phone` VARCHAR(50) CHARACTER SET utf8mb4 NULL DEFAULT null,
        `email` VARCHAR(50) CHARACTER SET utf8mb4 NULL DEFAULT null,
        `utc_created_date` DATETIME NOT NULL,
        `utc_updated_date` DATETIME NULL DEFAULT null,
        `utc_deleted_date` DATETIME NULL DEFAULT null,
        CONSTRAINT `pk_clients` PRIMARY KEY (`client_id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE TABLE `invoices` (
        `invoice_id` BIGINT NOT NULL AUTO_INCREMENT,
        `guid` VARCHAR(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT (UUID()),
        `number` VARCHAR(30) CHARACTER SET utf8mb4 NOT NULL,
        `utc_date` DATETIME NOT NULL,
        `status` SMALLINT NOT NULL,
        `payment_term` SMALLINT NOT NULL,
        `bill_from_address_id` BIGINT NOT NULL,
        `bill_to_address_id` BIGINT NOT NULL,
        `client_id` BIGINT NOT NULL,
        `utc_created_date` DATETIME NOT NULL,
        `utc_updated_date` DATETIME NULL DEFAULT null,
        `utc_deleted_date` DATETIME NULL DEFAULT null,
        CONSTRAINT `pk_invoices` PRIMARY KEY (`invoice_id`),
        CONSTRAINT `fk_invoices_addresses_bill_from_address_id` FOREIGN KEY (`bill_from_address_id`) REFERENCES `addresses` (`address_id`) ON DELETE CASCADE,
        CONSTRAINT `fk_invoices_addresses_bill_ftom_address_id` FOREIGN KEY (`bill_to_address_id`) REFERENCES `addresses` (`address_id`) ON DELETE CASCADE,
        CONSTRAINT `fk_invoices_clients_client_id` FOREIGN KEY (`client_id`) REFERENCES `clients` (`client_id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE TABLE `invoice_items` (
        `invoice_item_id` BIGINT NOT NULL AUTO_INCREMENT,
        `invoice_id` BIGINT NOT NULL,
        `description` VARCHAR(128) CHARACTER SET utf8mb4 NOT NULL,
        `quantity` SMALLINT NOT NULL,
        `amount` DECIMAL(19,4) NOT NULL,
        `utc_created_date` DATETIME NOT NULL,
        `utc_updated_date` DATETIME NULL DEFAULT null,
        `utc_deleted_date` DATETIME NULL DEFAULT null,
        CONSTRAINT `pk_invoice_item` PRIMARY KEY (`invoice_item_id`),
        CONSTRAINT `fk_invoice_item_invoice_invoice_id` FOREIGN KEY (`invoice_id`) REFERENCES `invoices` (`invoice_id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE INDEX `ix_addresses_country_code` ON `addresses` (`country_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE INDEX `ix_addresses_cty` ON `addresses` (`city`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE INDEX `ix_addresses_region` ON `addresses` (`region`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE UNIQUE INDEX `ux_addresses_address` ON `addresses` (`address_line_1`, `city`, `region`, `postal_code`, `country_code`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE INDEX `Iix_client_guid` ON `clients` (`guid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE UNIQUE INDEX `ux_client_name` ON `clients` (`name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE INDEX `IX_invoice_items_invoice_id` ON `invoice_items` (`invoice_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE UNIQUE INDEX `ux_invoice_items_description` ON `invoice_items` (`description`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE UNIQUE INDEX `IX_invoices_bill_from_address_id` ON `invoices` (`bill_from_address_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE UNIQUE INDEX `IX_invoices_bill_to_address_id` ON `invoices` (`bill_to_address_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE INDEX `IX_invoices_client_id` ON `invoices` (`client_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    CREATE UNIQUE INDEX `ux_invoices_number` ON `invoices` (`number`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230705201527_InitialMigration') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20230705201527_InitialMigration', '6.0.19');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

