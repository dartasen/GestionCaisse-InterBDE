-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema bde
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema bde
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `bde` DEFAULT CHARACTER SET utf8 ;
USE `bde` ;

-- -----------------------------------------------------
-- Table `bde`.`Produit`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `bde`.`Produit` ;

CREATE TABLE IF NOT EXISTS `bde`.`Produit` (
  `idProduit` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `nom` VARCHAR(50) NOT NULL,
  `prix` DOUBLE UNSIGNED NOT NULL,
  `prixAchat` DOUBLE UNSIGNED NOT NULL,
  `quantite` INT UNSIGNED NOT NULL,
  `categorie` ENUM('boisson', 'snack') NOT NULL,
  `datePeremption` DATE NULL,
  UNIQUE INDEX `nom_UNIQUE` (`nom` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `bde`.`Bde`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `bde`.`Bde` ;

CREATE TABLE IF NOT EXISTS `bde`.`Bde` (
  `idBde` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `nom` VARCHAR(25) NOT NULL,
  `taux` DOUBLE UNSIGNED NOT NULL,
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `bde`.`Utilisateur`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `bde`.`Utilisateur` ;

CREATE TABLE IF NOT EXISTS `bde`.`Utilisateur` (
  `idUtilisateur` INT NOT NULL AUTO_INCREMENT,
  `nom` VARCHAR(50) NOT NULL,
  `codePersonnel` CHAR(32) NOT NULL,
  `codeBadge` VARCHAR(45) NULL,
  `idBde` INT NOT NULL,
  `admin` TINYINT(1) NOT NULL,
  `active` TINYINT(1) NOT NULL,
  PRIMARY KEY (`idUtilisateur`),
  INDEX `id BDE existant_idx` (`idBde` ASC),
  CONSTRAINT `UtilisateurIdBDEInexistant`
    FOREIGN KEY (`idBde`)
    REFERENCES `bde`.`Bde` (`idBde`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `bde`.`Historique`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `bde`.`Historique` ;

CREATE TABLE IF NOT EXISTS `bde`.`Historique` (
  `idVente` INT NOT NULL AUTO_INCREMENT,
  `idUtilisateur` INT NULL,
  `idProduit` INT NOT NULL,
  `quantite` INT UNSIGNED NOT NULL,
  `idBDEAcheteur` INT NOT NULL,
  `dateVente` DATETIME NOT NULL,
  PRIMARY KEY (`idVente`),
  INDEX `Id utilisateur existant_idx` (`idUtilisateur` ASC),
  INDEX `Id produit existant_idx` (`idProduit` ASC),
  INDEX `Id BDE existant_idx` (`idBDEAcheteur` ASC),
  CONSTRAINT `HistoriqueIdUtilisateurInexistant`
    FOREIGN KEY (`idUtilisateur`)
    REFERENCES `bde`.`Utilisateur` (`idUtilisateur`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `HistoriqueIdProduitInexistant`
    FOREIGN KEY (`idProduit`)
    REFERENCES `bde`.`Produit` (`idProduit`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `HistoriqueIdBDEInexistant`
    FOREIGN KEY (`idBDEAcheteur`)
    REFERENCES `bde`.`Bde` (`idBde`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `bde`.`Produit`
-- -----------------------------------------------------
START TRANSACTION;
USE `bde`;
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Twix', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'M&M\'s', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Skittles', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Kinder Bueno White', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Kinder Bueno', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Lion', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Mars', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Snickers', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'Bounty', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (NULL, 'KitKat', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Smarties', 0.70, 0.70, 20, 'snack', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Orangina', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Sprite', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Energie Drink', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Fanta Orange', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Volvic Citron', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Lipton Ice Tea Pêche', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Oasis Orange', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Oasis Pomme Cassis Framboise', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Oasis Tropical', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Coca-Cola', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Coca-Cola Cherry', 0.70, 0.70, 20, 'boisson', NULL);
INSERT INTO `bde`.`Produit` (`idProduit`, `nom`, `prix`, `prixAchat`, `quantite`, `categorie`, `datePeremption`) VALUES (DEFAULT, 'Mr. Freeze', 0.50, 0.70, 20, 'snack', NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `bde`.`Bde`
-- -----------------------------------------------------
START TRANSACTION;
USE `bde`;
INSERT INTO `bde`.`Bde` (`idBde`, `nom`, `taux`) VALUES (NULL, 'Informatique', 0.5);
INSERT INTO `bde`.`Bde` (`idBde`, `nom`, `taux`) VALUES (NULL, 'Biologie', 0.25);
INSERT INTO `bde`.`Bde` (`idBde`, `nom`, `taux`) VALUES (NULL, 'RT', 0.25);

COMMIT;


-- -----------------------------------------------------
-- Data for table `bde`.`Utilisateur`
-- -----------------------------------------------------
START TRANSACTION;
USE `bde`;
INSERT INTO `bde`.`Utilisateur` (`idUtilisateur`, `nom`, `codePersonnel`, `codeBadge`, `idBde`, `admin`, `active`) VALUES (NULL, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'NULL', 1, 1, 1);

COMMIT;

