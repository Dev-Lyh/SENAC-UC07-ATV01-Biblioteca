CREATE TABLE usuarios (
    IdUsuario INT(4) AUTO_INCREMENT,
    NomeUsuario VARCHAR(80) NOT NULL,
    SenhaUsuario VARCHAR(80) NOT NULL,
    PRIMARY KEY(IdUsuario)
);

INSERT INTO `usuarios`(`NomeUsuario`, `SenhaUsuario`) VALUES ('admin', MD5('123'));