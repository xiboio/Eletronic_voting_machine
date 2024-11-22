create database bolsonaro;
use bolsonaro;

create table candidatos(
Id_Cand int primary key auto_increment,
Num_Voto int not null,
Nome varchar(60) not null,
Idade int not null,
Partido varchar(70) not null,
Cargo varchar(50) not null
);

SET FOREIGN_KEY_CHECKS = 0;
ALTER TABLE candidatos MODIFY COLUMN Id_Cand INT NOT NULL AUTO_INCREMENT;
SET FOREIGN_KEY_CHECKS = 1;

ALTER TABLE candidatos
ADD COLUMN Foto LONGBLOB;

create table eleitores(
Cpf varchar(11) primary key,
Nome varchar(70) not null,
Idade int not null,
Senha varchar(70) not null,
voto int,
foreign key (voto) references candidatos(Id_Cand)
);

create table ADM(
Id int primary key not null auto_increment,
nome varchar(60) not null,
senha varchar(60) not null
);

insert into ADM(nome, senha) values ("ADM supremo", "12345678");

select * from candidatos