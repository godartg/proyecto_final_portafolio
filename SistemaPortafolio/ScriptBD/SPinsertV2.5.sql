DBCC CHECKIDENT (TipoUsuario, RESEED, 0)
TRUNCATE TABLE TipoUsuario

insert into Semestre values ('2019-I','2019','01/03/2019','05/07/2019','Activo')
insert into Semestre values ('2019-II','2019','05/08/2019','06/12/2019','Activo')


insert into Ciclo VALUES ('I ciclo','Primer ciclo','Activo')
insert into Ciclo VALUES ('II ciclo ','Segundo ciclo','Activo')
insert into Ciclo VALUES ('III ciclo','Tercer ciclo','Activo')
insert into Ciclo VALUES ('IV ciclo','Cuarto ciclo','Activo')
insert into Ciclo VALUES ('V ciclo','Quinto ciclo','Activo')
insert into Ciclo VALUES ('VI ciclo','Sexto ciclo','Activo')
insert into Ciclo VALUES ('VII ciclo','Septimo ciclo','Activo')
insert into Ciclo VALUES ('VIII ciclo','Octavo ciclo','Activo')
insert into Ciclo VALUES ('IX ciclo','Noveno ciclo','Activo')
insert into Ciclo VALUES ('X ciclo','Decimo ciclo','Activo')

insert into PlanEstudio VALUES('1','Plan de Estudio 2019-I','Activo');

insert into CURSO VALUES ('ING-101','1','1','MATEMATICA I','5','4','2','6','Ninguno','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-102','1','1','MATEMATICA BASICA I','5','4','2','6','Ninguno','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-103','1','1','DISEÑO EN INGENIERIA ','4','2','4','6','Ninguno','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-104','1','1','COMUNICACION ORAL Y ESCRITA','3','2','2','4','Ninguno','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-105','1','1','METODOLOGIA DEL TRABAJO UNIVERSITARIO','3','2','2','4','Ninguno','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-171','1','1','INTRODUCCION A LA INGENIERIA DE SISTEMAS','2','2','0','2','Ninguno','Activo','Obligatorio', 'A');

insert into CURSO VALUES ('ING-201','1','2','MATEMATICA II','5','4','2','6','ING-101','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-202','1','2','FISICA I','5','4','2','6','Min 14 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-203','1','2','TECNICAS DE PROGRAMACION ','4','2','4','6','Ninguno','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-204','1','2','ECONOMIA I','2','1','2','3','Min 12 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-205','1','2','ESTADISTICA I','3','2','2','4','Min 12 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('ING-206','1','2','QUIMICA I','4','3','2','5','Ninguno','Activo','Obligatorio', 'A');

insert into CURSO VALUES ('SI-371','1','3','MATEMATICA DISCRETA','4','2','4','6','ING-201','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-372','1','3','SISTEMAS ELECTRONICOS DIGITALES','4','2','4','6','ING-202','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-373','1','3','ALGORITMOS Y ESTRUCTURA DE DATOS ','4','2','4','6','ING-203','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-374','1','3','DISEÑO Y MODELAMIENTO VIRTUAL','3','2','2','4','ING-103','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-375','1','3','MODELO DE PROCESOS DE NEGOCIOS','4','2','4','6','Min 36 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-376','1','3','SISTEMAS DE INFORMACION','3','2','2','4','Min 36 creditos','Activo','Obligatorio', 'A');

insert into CURSO VALUES ('SI-471','1','4','INTRODUCCION AL DESARROLLO WEB','3','2','2','4','Min 60 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-472','1','4','ARQUITECTURA DEL COMPUTADOR','4','2','4','6','SI-372','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-473','1','4','PROGRAMACION I','4','2','4','6','SI-373','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-474','1','4','INGENIERIA ECONOMICA Y FINANCIERA','3','2','2','4','SI-371','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-475','1','4','INGENIERIA DE REQUERIMIENTOS','4','2','4','6','SI-375','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-476','1','4','PROGRAMACION ORIENTADA A OBJETOS','4','2','4','6','Min 60 creditos','Activo','Obligatorio', 'A');

insert into CURSO VALUES ('SI-571','1','5','DISEÑO DE BASE DE DATOS','4','2','4','6','SI-475','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-572','1','5','SISTEMAS OPERATIVOS I','4','2','4','6','SI-472','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-573','1','5','PROGRAMACION II','4','2','4','6','SI-473','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-574','1','5','INVESTIGACION DE OPERACIONES','3','2','2','4','SI-474','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-575','1','5','DISEÑO Y ARQUITECTURA DE SOFTWARE','4','2','4','6','SI-475','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-576','1','5','INTERACCION Y DISEÑO DE INTERFACES','3','2','2','4','Min 80 creditos','Activo','Obligatorio', 'A');

insert into CURSO VALUES ('SI-671','1','6','BASE DE DATOS I','4','2','4','6','SI-571','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-672','1','6','SISTEMAS OPERATIVOS II','4','2','4','6','SI-572','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-673','1','6','PROGRAMACION III','4','2','4','6','SI-573','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-674','1','6','DESARRROLLO DE APLICACIONES WEB I','4','2','4','6','SI-571','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-675','1','6','INGENIERIA DE SOFTWARE','3','2','2','4','SI-575','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-676','1','6','ETICA PROFESIONAL','3','3','0','3','Min 100 creditos','Activo','Obligatorio', 'A');

insert into CURSO VALUES ('SI-771','1','7','BASE DE DATOS II','4','2','4','6','SI-671','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-772','1','7','REDES Y COMUNICACIONES DE DATOS I','3','2','2','4','SI-672','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-773','1','7','SOLUCIONES MOVILES I','4','2','4','6','SI-673','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-774','1','7','GESTION DE PROYECTOS DE TI','4','2','4','6','SI-675','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-775','1','7','CALIDAD Y PRUEBAS DE SOFTWARE','4','2','4','6','Min 120 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-776','1','7','MEDIO AMBIENTE Y DESARROLLO SOSTENIBLE','3','2','2','4','Min 120 creditos','Activo','Electivo', 'A');
insert into CURSO VALUES ('SI-777','1','7','CONTABILIDAD GENERAL','3','2','2','4','Min 120 creditos','Activo','Electivo', 'A');
insert into CURSO VALUES ('SI-778','1','7','PATRONES DE SOFTWARE','3','2','2','4','SI-575','Activo','Electivo', 'A');

insert into CURSO VALUES ('SI-871','1','8','INTELIGENCIA DE NEGOCIOS','4','2','4','6','SI-771','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-872','1','8','REDES Y COMUNICACIONES DE DATOS II','4','2','4','6','SI-772','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-873','1','8','INGLES TECNICO','4','2','4','6','(*)','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-874','1','8','DESARRROLLO DE APLICACIONES WEB II','4','2','4','6','SI-674','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-875','1','8','SEGURIDAD INFORMATICA','4','3','2','5','Min 140 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-876','1','8','DERECHO INFORMATICO','3','2','2','4','Min 140 creditos','Activo','Electivo', 'A');
insert into CURSO VALUES ('SI-877','1','8','DISEÑO Y CREACION DE VIDEOJUEGOS','3','2','2','4','SI-673','Activo','Electivo', 'A');

insert into CURSO VALUES ('SI-971','1','9','AUDITORIA DE SISTEMAS','3','2','2','4','SI-875','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-972','1','9','REDES Y COMUNICACIONES DE DATOS III','4','2','4','6','SI-872','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-973','1','9','METODOLOGIA DE LA INVESTIGACION APLICADA','3','2','2','4','SI-873','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-974','1','9','CONSTRUCCION DE SOFTWARE I','4','2','4','6','SI-774','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-975','1','9','PLANEAMIENTO ESTRATEGICO DE TI','4','3','2','5','Min 160 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-976','1','9','GESTION DE LA CONFIGURACION Y ADMINISTRACION DE SOFTWARE','3','2','2','4','SI-775','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-977','1','9','NEGOCIOS Y MARKETING POR INTERNET','3','2','2','4','Min 160 creditos','Activo','Electivo', 'A');
insert into CURSO VALUES ('SI-978','1','9','TOPICOS DE BASE DE DATOS AVANZADOS','3','2','2','4','SI-871','Activo','Electivo', 'A');

insert into CURSO VALUES ('SI-071','1','10','TALLER DE LIDERAZGO Y EMPRENDIMIENTO','3','2','2','4','Min 180 creditos','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-072','1','10','TALLER REDES Y COMUNICACION DE DATOS ','4','2','4','6','SI-972','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-073','1','10','PROYECTO DE TESIS','4','2','4','6','SI-973','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-074','1','10','CONSTRUCCION DE SOFTWARE II','4','2','4','6','SI-974','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-075','1','10','GERENCIA DE TI','3','2','2','4','SI-975','Activo','Obligatorio', 'A');
insert into CURSO VALUES ('SI-076','1','10','SISTEMAS DE INFORMACION DE BANCA Y FINANZAS','3','2','2','4','Min 180 creditos','Activo','Electivo', 'A');
insert into CURSO VALUES ('SI-077','1','10','SOLUCIONES MOVILES II','3','2','2','4','SI-773','Activo','Electivo', 'A');

insert into TipoPersona VALUES('Docente','Activo');
insert into TipoPersona VALUES('Alumno','Activo');

insert into Persona values(1,'00435755','00858052','Silvia Marlene','Centella Vildoso','smcentella@upt.pe','052411749','Activo')
insert into Persona values(1,'00514322','00995308','Felipe Remigio','Atencio Maquera','fratencio@upt.pe','952525252','Activo')
insert into Persona values(1,'00514322','00995308','Nelida Brigida','Maquera Cardenas','nbmaquera@upt.pe','981524639','Activo')
insert into Persona values(1,'00514999','00994853','Claudia Susy','Alvarez Sanchez','csalvarez@upt.pe','966248571','Activo')
insert into Persona values(1,'00514999','00994853','Lourdes Vanessa','Revollar Vildoso','lvrevollar@upt.pe','952774488','Activo')
insert into Persona values(1,'00794499','00994680','Americo','Alca Gomez','a_alca_g@hotmail.com','984127451','Activo')
insert into Persona values(1,'00421817','00994853','Mariella Carmen','Berrios Flores','mcberrios@upt.pe','964257183','Activo')
insert into Persona values(1,'00793868','00994338','Yanira Maria','Valdivia Tapia','yaniravaldivia@yahoo.es','952481478','Activo')
insert into Persona values(1,'00792755','00995307','Oliver','Santana Carbajal','olsantana@upt.pe','945178290','Activo')
insert into Persona values(1,'43389061','00995156','Nestor Andres','Sanjinez Ticona','nasanjinez@upt.pe','961245873','Activo')
insert into Persona values(1,'00498513','00156834','Liliana Mercedes Milagros','Vega Bernal','lilianavegabernal@gmail.com','952648778','Activo')
insert into Persona values(1,'04743075','00782190','Elard Ricardo','Rodriguez Marca','elardroma@hotmail.com','965876879','Activo')
insert into Persona values(1,'04647727','00994622','Maritza Marleni','Catari Cutipa','mmcatari@upt.pe','981406231','Activo')
insert into Persona values(1,'00795082','00994888','Alex Juan','Yanqui Constancio','ajyanqui@upt.pe','952004141','Activo')
insert into Persona values(1,'43303662','00994834','Milagros Gleny','Cohaila Gonzales','mgcohaila@upt.pe','981232323','Activo')
insert into Persona values(1,'00483804','00234567','Mariella Rosario','Ibarra Montecinos','mariellaibarra@yahoo.es','960412574','Activo')
insert into Persona values(1,'00662102','00260467','Tito Fernando','Ale Nieto','titofale@gmail.com','952630000','Activo')
insert into Persona values(1,'40002378','00459756','Enrique Felix','Lanchipa Valencia','enrique_tacna@hotmail.com','965875421','Activo')
insert into Persona values(1,'42849817','00994892','Alberto Johnatan','Flor Rodriguez','alflor@egx.com','986656532','Activo')
insert into PERSONA VALUES(1,'00493789','00895318','Carlos Alberto','Ruiz Cancino','carlosruiz77@gmail.com','962341578','Activo');
insert into Persona values(1,'00489462','00298907','Martha Judith','Paredes Vignola','martijudi@hotmail.com','999545254','Activo')
insert into Persona values(1,'00475920','00409196','Erbert Francisco','Osco Mamani','erbert_o@yahoo.es','998747474','Activo')
insert into PERSONA VALUES(1,'41827083','00994479','Patrick Jose','Cuadros Quiroga','patrick_jcq@hotmail.com','960818080','Activo');
insert into Persona values(1,'00486155','00248025','Hugo Martin','Alcantara Martinez','martin_alcantara@hotmail.com','952345487','Activo')
insert into PERSONA VALUES(1,'00480798','00513186','Rafael Humberto','Poma Laura','rpomalaura@gmail.com','980236541','Activo');
insert into PERSONA VALUES(1,'00491898','00325981','Ricardo Manuel','Sante Zavaleta','rzsante@hotmail.com','981245870','Activo');
insert into PERSONA VALUES(1,'00797310','00995158','Hiraida Yesenia','Pacheco Quispe','hypacheco@upt.pe','999888888','Activo');
insert into PERSONA VALUES(1,'08652249','00410947','Carlos Alberto','Pajuelo Beltran','pajuelocarlos@gmail.com','922441177','Activo');
insert into PERSONA VALUES(1,'00471099','00994606','Oscar Juan','Jimenez Flores','ojjimenez@hotmail.com','952784519','Activo');
insert into Persona values(1,'00498367','00624536','Luis Alfredo','Fernandez Vizcarra','lfvfernandez@gmail.com','981904848','Activo')
insert into PERSONA VALUES(1,'00499346','00550694','Ricardo Manuel','Valcarcel Alvarado','revasoft00@hotmail.com','952789652','Activo');

insert into Persona values(2,'79443957','2013000210','Edwin César','Condori Vilcapuma','eccondori@upt.pe','958585858','Activo')
insert into Persona values(2,'79443958','2013000210','Cristian','Cespedes','crcespedes@upt.pe','958585858','Activo')
insert into Persona values(2,'44410892','2004025392','Carol Fiorella','Husnayo Cutipa','cfhusnayo@upt.pe','958585858','Activo')
insert into Persona values(2,'79443959','2013000210','Rosalia','Mamani Llaca','rmamani@upt.pe','958585858','Activo')
insert into Persona values(2,'72463960','2011239244','Eduardo','Ayca Mamani','emamania@hotmail.com','952020240','Activo')
insert into PERSONA VALUES(2,'70748140','2013000209','Lucero','Gonzales Perez','lucerogoga63@hotmail.com','990079772','Activo');
insert into PERSONA VALUES(2,'70788545','2014049092','Andrea','Faucheux Mora','andrea.ale543@hotmail.com','952969595','Activo');
insert into PERSONA VALUES(2,'70101010','2013000290','Angelica','Milla Juarez','amilla@hotmail.com','952969595','Activo');
insert into PERSONA VALUES(2,'79095500','2013000999','Guido','Pacsi Asencio','gpacsi@hotmail.com','952119325','Activo');
insert into PERSONA VALUES(2,'70748523','2013000215','Sergio','Perez Rios','sperez@hotmail.com','952969595','Activo');

insert into CursoDocente values(1,1)
insert into CursoDocente values(2,3)
insert into CursoDocente values(3,4)
insert into CursoDocente values(4,6)
insert into CursoDocente values(5,8)
insert into CursoDocente values(6,9)

insert into CursoDocente values(7,3)
insert into CursoDocente values(8,2)
insert into CursoDocente values(9,10)
insert into CursoDocente values(10,11)
insert into CursoDocente values(11,12)
insert into CursoDocente values(12,13)

insert into CursoDocente values(13,1)
insert into CursoDocente values(14,14)
insert into CursoDocente values(15,15)
insert into CursoDocente values(16,10)
insert into CursoDocente values(17,16)
insert into CursoDocente values(18,9)

insert into CursoDocente values(19,17)
insert into CursoDocente values(20,14)
insert into CursoDocente values(21,10)
insert into CursoDocente values(22,11)
insert into CursoDocente values(23,16)
insert into CursoDocente values(24,12)

insert into CursoDocente values(25,9)
insert into CursoDocente values(26,18)
insert into CursoDocente values(27,12)
insert into CursoDocente values(28,15)
insert into CursoDocente values(29,19)
insert into CursoDocente values(30,10)

insert into CursoDocente values(31,20)
insert into CursoDocente values(32,12)
insert into CursoDocente values(33,18)
insert into CursoDocente values(34,17)
insert into CursoDocente values(35,21)
insert into CursoDocente values(36,22)

insert into CursoDocente values(37,23)
insert into CursoDocente values(38,24)
insert into CursoDocente values(39,19)
insert into CursoDocente values(40,21)
insert into CursoDocente values(41,25)
insert into CursoDocente values(42,22)
insert into CursoDocente values(43,4)
insert into CursoDocente values(44,5)

insert into CursoDocente values(45,23)
insert into CursoDocente values(46,26)
insert into CursoDocente values(47,27)
insert into CursoDocente values(48,18)
insert into CursoDocente values(49,9)
insert into CursoDocente values(50,28)
insert into CursoDocente values(51,4)

insert into CursoDocente values(52,29)
insert into CursoDocente values(53,24)
insert into CursoDocente values(54,30)
insert into CursoDocente values(55,25)
insert into CursoDocente values(56,25)
insert into CursoDocente values(57,31)
insert into CursoDocente values(58,20)
insert into CursoDocente values(59,5)

insert into CursoDocente values(60,11)
insert into CursoDocente values(61,24)
insert into CursoDocente values(62,30)
insert into CursoDocente values(63,19)
insert into CursoDocente values(64,31)
insert into CursoDocente values(65,29)
insert into CursoDocente values(66,4)


insert into TIPOUSUARIO VALUES('Administrador','Ingresa a todos los modulos','Activo');
insert into TIPOUSUARIO VALUES('Usuario','Ingresa a algunos modulos','Activo');

insert into USUARIO VALUES(18,1,'elanchipa','e10adc3949ba59abbe56e057f20f883e','user_default.png','Activo');
insert into USUARIO VALUES(19,2,'aflor','e10adc3949ba59abbe56e057f20f883e','user_default.png','Activo');

insert into HOJAVIDA values(1);
insert into HojaVidaDocenteFA values(1,'Institucion Prueba','Titulo Prueba', '2010');
insert into HojaVidaDocenteFC values(1,'Institucion Prueba','320 horas','60 dias','Curso Prueba');
insert into HojaVidaDocenteCRP values(1,'Certificacion Prueba','Institucion Prueba','2005');
insert into HojaVidaDocenteEX values(1,'Institucion Prueba','05/06/2006','05/12/2006','Funcion Prueba', 'academica');
insert into HojaVidaDocenteEX values(1,'Institucion Prueba','05/06/2006','05/12/2006','Funcion Prueba2', 'no academica');

INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.docx', 'Curriculum Vitae ICACIT');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.docx', 'Silabo');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.xlsx', 'Prueba de Entrada');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.xlsx', 'Portafolio');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.docx;.pptx', 'Recursos Docentes');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.docx;.pptx', 'Recursos Estudiantes');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 1, '.docx', 'Guias de Laboratorio y Talleres');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 2, '.docx;.pptx;.xlsx', 'Otros Recursos Estudiantes');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 2, '.docx', 'Examenes');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES ( 2, '.xlsx', 'Practicas calificadas');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (2, '.xlsx', 'Trabajos');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (2, '.xlsx', 'Proyecto final');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Informe Final del Curso');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Informe');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Trabajo de Investigacion');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Laboratorio');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Solución Examen');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.pptx', 'Dipositivas');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Guías de Laboratorio');
INSERT INTO db_portafolio2.dbo.TipoDocumento (tipopersona_id, extension, nombre) VALUES (1, '.docx', 'Otros Recursos Docentes');

insert into UNIDAD VALUES(1,'I','Activo');
insert into UNIDAD VALUES(1,'II','Activo');
insert into UNIDAD VALUES(1,'III','Activo');

insert into ConfigEntrega VALUES(1,'Configuracion de Entrega Unidad I','01/06/2019','05/07/2019','Activo');
insert into ConfigEntrega VALUES(2,'Configuracion de Entrega Unidad II','01/06/2019','05/07/2019','Activo');
insert into ConfigEntrega VALUES(3,'Configuracion de Entrega Unidad III','01/06/2019','05/07/2019','Activo');

insert into Notificacion VALUES(4,1,'¡Urgente!','Cambio de Documentos','Debe cambiar los documentos marcados como Rechazado','01/07/2019','No Leido');
