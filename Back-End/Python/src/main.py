import blob;
import aladb;

from fastapi import FastAPI, Request, Response
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
import uvicorn
import multiprocessing
import os
import base64


try:
    DBINTERFACE = aladb.crudinterface((os.environ.get("DB"))) 
except:
    exit();
    pass;



app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class _http_crr_ncscd_usr(BaseModel):
    username: str
    password: str
    nombre: str
    nacimiento: str
    nota: str
    rol: int

@app.post("/api/v1/crear_encascada_usuario/")
async def http_crear_encascada_usuario(entrada: _http_crr_ncscd_usr, response: Response):
    res = blob.buscar_nombreusuario(DBINTERFACE,entrada.username);
    if(res != None):
        response.status_code = 409;
        return
        

    res =blob.crear_encascada_usuario(DBINTERFACE,entrada.username,entrada.password,entrada.nombre,entrada.nacimiento,entrada.nota,entrada.rol)
    if(res!=None):
        response.status_code = 201;
    else:
        response.status_code = 400;
    return


def _encodeb64(entrada):
    sample_string = entrada
    sample_string_bytes = sample_string.encode("ascii")
    base64_bytes = base64.b64encode(sample_string_bytes)
    base64_string = base64_bytes.decode("ascii")
    return base64_string;

class _http_inicrssn_usr(BaseModel):
    username: str
    password: str

@app.post("/api/v1/iniciarsesion_usuario/")
async def http_iniciarsesion_usuario(entrada: _http_inicrssn_usr, response: Response):
    res = blob.iniciarsesion_usuario(DBINTERFACE,entrada.username,entrada.password)
    if(res!=None):
        response.status_code = 200;
        return {'Token':_encodeb64(entrada.username+":"+str(res[0]))}
    else:
        response.status_code = 401;
    pass


def _decode64(entrada):
    print("entrada")
    base64_string =entrada
    base64_bytes = base64_string.encode("ascii")
    sample_string_bytes = base64.b64decode(base64_bytes)
    sample_string = sample_string_bytes.decode("ascii")
    return sample_string

def _verificartokendeheader(tokendeheader):
    tkn = tokendeheader.split(" ")
    tkn = _decode64(tkn[1])
    usrytkn = tkn.split(":")
    print(usrytkn)
    return blob.autenticartoken_usuario(DBINTERFACE,usrytkn[0],usrytkn[1])
    

class _http_authtkn_usr(BaseModel):
    msg: str

@app.post("/api/v1/autenticartoken_usuario/post/")
async def http_autenticartoken_usuario(entrada: _http_authtkn_usr, response: Response, request: Request):
    HAuth = (request.headers.get('Authorization'));
    try:
        res = _verificartokendeheader((HAuth))
        if(res != None):
            response.status_code = 200;
            return {'MSG': entrada.msg};
    except:
        pass    
    response.status_code = 401
    return



if(__name__=='__main__'):
    multiprocessing.freeze_support()
    uvicorn.run("main:app", host="0.0.0.0", port=int(os.environ.get("PORT")), reload=False,workers=1)
