import uuid;
import aladb;
import hashlib;

def _hashpassword(u,p):
    m = hashlib.sha256();
    m.update((u+p+"SAL").encode("ascii"));
    return m.hexdigest();

def buscar_nombreusuario(dbinterface,username):
    lector = dbinterface.accesotoken_username.lector(username);
    data = lector.leer(1)[0]
    return data;

def crear_encascada_usuario(dbinterface,username,password,nombre,nacimiento,nota,rol):
    prsn = dbinterface.persona.crear(nombre,nacimiento,nota)
    prms = dbinterface.permisos.crear(rol);
    prfl = dbinterface.perfil.crear(prms,prsn);
    accs = dbinterface.acceso.crear(_hashpassword(username,password),username,prfl);
    return accs;

def iniciarsesion_usuario(dbinterface,username,password):
    lector = dbinterface.acceso.lector(username,_hashpassword(username,password));
    data = lector.leer(1)[0];
    token = uuid.uuid4().hex;
    lector = dbinterface.accesotoken.actualizar(token,data[0]);
    return token,data[1];

def autenticartoken_usuario(dbinterface,username,token):
    lector = dbinterface.accesotoken.lector(username,token);
    data = lector.leer(1);
    return data;


