-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 12, 2024 at 02:51 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cccp_a1`
--
CREATE DATABASE IF NOT EXISTS `cccp_a1` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `cccp_a1`;

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

CREATE TABLE `employee` (
  `EmployeeID` int(11) NOT NULL,
  `EmployeeName` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`EmployeeID`, `EmployeeName`) VALUES
(1, 'Graham'),
(2, 'Phil'),
(3, 'Jan');

-- --------------------------------------------------------

--
-- Table structure for table `item`
--

CREATE TABLE `item` (
  `ItemID` int(11) NOT NULL,
  `ItemQty` int(11) DEFAULT NULL,
  `ItemName` varchar(255) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `item`
--

INSERT INTO `item` (`ItemID`, `ItemQty`, `ItemName`, `DateCreated`) VALUES
(1, 10, 'Pencil', '2024-01-12 00:57:11'),
(2, 18, 'Eraser', '2024-01-12 00:57:11');

-- --------------------------------------------------------

--
-- Table structure for table `transaction_log_entry`
--

CREATE TABLE `transaction_log_entry` (
  `TransactionID` int(11) NOT NULL,
  `TransactionType` varchar(255) NOT NULL,
  `ItemID` int(11) NOT NULL,
  `ItemName` varchar(255) NOT NULL,
  `ItemPrice` double NOT NULL,
  `TransactionQty` int(11) NOT NULL,
  `EmployeeName` varchar(255) NOT NULL,
  `TransactionDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transaction_log_entry`
--

INSERT INTO `transaction_log_entry` (`TransactionID`, `TransactionType`, `ItemID`, `ItemName`, `ItemPrice`, `TransactionQty`, `EmployeeName`, `TransactionDate`) VALUES
(1, 'Item Added', 1, 'Pencil', 0.25, 10, 'Graham', '2024-01-12 00:57:11'),
(2, 'Item Added', 2, 'Eraser', 0.15000000596046448, 20, 'Phil', '2024-01-12 00:57:11'),
(3, 'Quantity Removed', 2, 'Eraser', -1, 4, 'Graham', '2024-01-12 00:57:11'),
(4, 'Quantity Added', 2, 'Eraser', 0.33, 2, 'Phil', '2024-01-12 00:57:11');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`EmployeeID`);

--
-- Indexes for table `item`
--
ALTER TABLE `item`
  ADD PRIMARY KEY (`ItemID`);

--
-- Indexes for table `transaction_log_entry`
--
ALTER TABLE `transaction_log_entry`
  ADD PRIMARY KEY (`TransactionID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `employee`
--
ALTER TABLE `employee`
  MODIFY `EmployeeID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `item`
--
ALTER TABLE `item`
  MODIFY `ItemID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `transaction_log_entry`
--
ALTER TABLE `transaction_log_entry`
  MODIFY `TransactionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
