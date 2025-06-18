create database dbCadastrarProduto ;
use dbCadastrarProduto;

create table tbUsuarios(
IdUser int primary key auto_increment,
Nome varchar(50) not null,
Email varchar(100) not null,
Senha varchar(50) not null);

create table tbProdutos(
IdProd int primary key auto_increment,
Nome varchar(50) not null,
Descricao varchar(100) not null,
Preco decimal(12,2) not null,
Quantidade int not null);

select * from tbProdutos;