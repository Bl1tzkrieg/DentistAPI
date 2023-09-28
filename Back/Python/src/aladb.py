import sqlite3
import uuid

class sqliteinterface:
    def __init__(self,sqlitefile):
        self.cur = None;
        self.con = sqlite3.connect(sqlitefile);
        if(self.con != None):
            self.cur = self.con.cursor();
        pass


class dblector:
    def __init__(self,res):
        self.res = res
        pass
    def leer(self,sz):
        return self.res.fetchmany(sz);

class _dbcrudgenerico:
    def __init__(self,dbinterface,c,r,u,d):
        self.dbinterface = dbinterface;
        self.c=c
        self.r=r
        self.u=u
        self.d=d
        pass

    def crear(self,*args):
        uuidx = uuid.uuid4().hex;
        _args = ((uuidx),)+args;
        self.dbinterface.cur.execute(self.c,_args);
        self.dbinterface.con.commit();
        return uuidx;
       
    def lector(self,*args):
        res = self.dbinterface.cur.execute(self.r,args)
        return dblector(res);

    def actualizar(self,*args):
        self.dbinterface.cur.execute(self.u,args)
        self.dbinterface.con.commit();
        return


class crudinterface:

    def __init__(self,sqldir):
        self.db = sqliteinterface(sqldir);
        self.persona = _dbcrudgenerico(self.db,"insert into persona(uuid,nombre,nacimiento,nota) values (?,?,?,?);","select nombre,nacimiento,nota from persona where uuid = ?;","","")
        self.permisos = _dbcrudgenerico(self.db,"insert into permisos (uuid,rol) values (?,?);","","","")
        self.perfil = _dbcrudgenerico(self.db,"insert into perfil (uuid,fk_permisos,fk_persona) values (?,?,?);","select fk_permisos,fk_persona from perfil where uuid = ?","","")
        self.acceso = _dbcrudgenerico(self.db,"insert into acceso (uuid,pass,username,fk_perfil) values (?,?,?,?);","select id,fk_perfil from acceso where username = ? and pass = ?","","");
        self.accesotoken = _dbcrudgenerico(self.db,self.acceso.c,"select fk_perfil from acceso where username = ? and token = ?","update acceso set token = ? where id = ?","")
        self.accesotoken_username = _dbcrudgenerico(self.db,"","select id from acceso where username = ?","","");
        self.historia = _dbcrudgenerico(self.db,"insert into historia(uuid,fk_persona) values (?,?);","select id from historia where fk_persona = ?","","")
        pass



