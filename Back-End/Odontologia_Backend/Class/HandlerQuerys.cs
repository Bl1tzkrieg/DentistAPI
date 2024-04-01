using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Odontologia_Backend.Data;
using Odontologia_Backend.DTOs;
using Odontologia_Backend.Models;

namespace Odontologia_Backend.Class
{
    public class HandlerQuerys
    {
        private readonly Context_MySql context_Mysql;

        public HandlerQuerys(Context_MySql dbMysql) 
        { 
            this.context_Mysql = dbMysql;
        }

        /// <summary>
        /// Devuelve una lista de consultas segun el estatus ingresado.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Object>> ObtenerConsultas(int status)
        {
            try
            {

                return await this.context_Mysql.consultas
                        .Join(this.context_Mysql.usuarios, c => c.Consult_Usu_Id, u => u.Usu_Id,
                            (consulta, usuario) => new { consulta, usuario })
                        .Join(this.context_Mysql.clientes, c => c.consulta.Consult_Id_Cliente, cli => cli.Cli_Id,
                            ( c2, cliente) => new { c2.consulta, c2.usuario, cliente})
                        .Where( q => ( status > 1 ? (status > 2 ? q.consulta.Consult_Facturada.Equals("POR FACTURAR") && q.consulta.Consult_Status.Equals("PENDIENTE") : q.consulta.Consult_Status.Equals("PENDIENTE")) : true ) )
                        .Select( x => new {
                            consulta_id = x.consulta.Consult_Id,
                            cedula_cliente = x.cliente.Cli_Cedula,
                            nombre_cliente = x.cliente.Cli_Nombre1,
                            apellido_cliente = x.cliente.Cli_Apellido1,
                            descripcion_consulta = x.consulta.Consult_Descripcion,
                            fecha_asignacion = x.consulta.Consult_FecAsig.ToString("dd/MM/yyyy"),
                            fecha_generacion = x.consulta.Consult_FecGen.ToString("dd/MM/yyyy"),
                            nombre_usuario = x.usuario.Usu_UserName,
                            consulta_estado = x.consulta.Consult_Status,
                            consulta_estado_pago = x.consulta.Consult_Facturada,
                        })
                        .ToListAsync<Object>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Devuelve una consulta especifica filtrada por su id
        /// </summary>
        /// <param name="id_consulta"></param>
        /// <returns></returns>
        public async Task<Object?> ObtenerConsulta( int id_consulta)
        {
            try
            {
                return await this.context_Mysql.consultas
                        .Join(this.context_Mysql.usuarios, c => c.Consult_Usu_Id, u => u.Usu_Id,
                            (consulta, usuario) => new { consulta, usuario })
                        .Join(this.context_Mysql.clientes, c => c.consulta.Consult_Id_Cliente, cli => cli.Cli_Id,
                            (c2, cliente) => new { c2.consulta, c2.usuario, cliente })
                        .Where(q => q.consulta.Consult_Id.Equals(id_consulta) )
                        .Select(x => new {
                            consulta_id = x.consulta.Consult_Id,
                            cedula_cliente = x.cliente.Cli_Cedula,
                            nombre_cliente = x.cliente.Cli_Nombre1,
                            apellido_cliente = x.cliente.Cli_Apellido1,
                            descripcion_consulta = x.consulta.Consult_Descripcion,
                            fecha_asignacion = x.consulta.Consult_FecAsig.ToString("dd/MM/yyyy"),
                            fecha_generacion = x.consulta.Consult_FecGen.ToString("dd/MM/yyyy"),
                            nombre_usuario = x.usuario.Usu_UserName,
                            consulta_estado = x.consulta.Consult_Status,
                            consulta_estado_pago = x.consulta.Consult_Facturada,
                            consulta_id_cliente = x.consulta.Consult_Id_Cliente,
                            consulta_facturada = x.consulta.Consult_Facturada
                        })
                        .FirstOrDefaultAsync<Object>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <summary>
        /// Registra una nueva consulta en la base de datos
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<(int,string)> RegistrarConsulta(Consulta consulta)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    await this.context_Mysql.consultas.AddAsync(consulta);
                    await this.context_Mysql.SaveChangesAsync();
                    await transactionMySql.CommitAsync();
                    return (1, "Consulta registrada con exito");
                }
                catch (Exception)
                {
                    await transactionMySql.RollbackAsync();
                    throw;
                }
            }
        }

        /// <summary>
        /// Actualiza una consulta con respecto a la fecha y description
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<(int,string)> ActualizarConsulta(Consulta consulta) 
        {
            try
            {
                Consulta? statusConsulta = await this.context_Mysql.consultas.FirstOrDefaultAsync(x => x.Consult_Id.Equals(consulta.Consult_Id));

                if (statusConsulta == null) return (0, "La consulta solicitada no se encontro.");

                statusConsulta.Consult_FecAsig = consulta.Consult_FecAsig;
                statusConsulta.Consult_Descripcion = consulta.Consult_Descripcion;

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Consulta actualizada correctamente");
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <summary>
        /// Factura una consulta.
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<(int,string)> FacturarConsulta(Query_Bill_Consultation_DTO consulta)
        {
            using (IDbContextTransaction transactionMySql = this.context_Mysql.Database.BeginTransaction())
            {
                try
                {
                    // primero se trae el registro relacionado con la consulta id
                    Consulta? statusConsulta = await this.context_Mysql.consultas.FirstOrDefaultAsync(x => x.Consult_Id.Equals(consulta.consulta_id));

                    if (statusConsulta == null) return (0, "Eventualidad inesperada la consulta solicitada no esta registrada!");

                    if (statusConsulta.Consult_Facturada.Equals("FACTURADA") || statusConsulta.Consult_Status.Equals("CANCELADA")) 
                        return (0, "Esta consulta ya se encuentra facturada o ha sido CAncelada");

                    Ventas_detalle statusVentaDetalle = new Ventas_detalle()
                    {
                        Vd_TipoDePago = consulta.tipo_pago,
                        Vd_Total = consulta.total_pagar
                    };

                    // procediendo a hacer un insert en venta detalle
                    await this.context_Mysql.ventas_detalle.AddAsync(statusVentaDetalle);
                    await this.context_Mysql.SaveChangesAsync();

                    // con el id recibido de venta_detalle luego del registro procedemos a registrar la venta.
                    await this.context_Mysql.ventas.AddAsync(new Venta()
                    {
                        Venta_Cli_Id = consulta.cliente_id,
                        Venta_FecGen = DateTime.Now,
                        Venta_NroFactura = consulta.numero_factura,
                        Venta_Usu_Id = consulta.usuario_id,
                        Venta_Vd_Id = statusVentaDetalle.Vd_Id,
                        Venta_Consult_Id = consulta.consulta_id,
                    });
                    await this.context_Mysql.SaveChangesAsync();

                    // luego de realizar el registro de la venta procedemos a actualizar la factura en su campo consult_facturada de "POR FACTURAR" a "FACTURADA"
                    statusConsulta.Consult_Facturada = "FACTURADA";
                    await this.context_Mysql.SaveChangesAsync();

                    // si no se han levantado excepciones o errores por incongruencias etc. procedemos a hacer el commit.
                    await transactionMySql.CommitAsync();
                    return (1, "Consulta facturada con exito");
                }
                catch (Exception)
                {
                    await transactionMySql.RollbackAsync();
                    throw;
                }
            }
        }

        /// <summary>
        /// Actualiza el status a una consulta como cancelada.
        /// </summary>
        /// <param name="consulta_id"></param>
        /// <returns></returns>
        public async Task<(int,string)> CancelarConsulta(int consulta_id)
        {
            try
            {
                Consulta? statusConsulta = await this.context_Mysql.consultas.FirstOrDefaultAsync(x => x.Consult_Id.Equals(consulta_id));

                if (statusConsulta == null) return (0, "Recurso no encontrado");

                if (statusConsulta.Consult_Facturada.Equals("FACTURADA")) return (0, "Esta consulta no puede ser cancelada porque ya ha sido facturada");

                statusConsulta.Consult_Status = "CANCELADA";

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Consulta cancelada satisfactoriamente");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// procesa una consulta, para cambiar su estado de PENDIENTE -> PROCESADA
        /// </summary>
        /// <param name="consulta_id"></param>
        /// <returns></returns>
        public async Task<(int,string)> ProcesarConsulta(int consulta_id)
        {
            try
            {
                Consulta? statusConsulta = await this.context_Mysql.consultas.FirstOrDefaultAsync(q => q.Consult_Id.Equals(consulta_id));

                if (statusConsulta == null) return (0, "No se pudo encontrar la consulta");

                if (statusConsulta.Consult_Facturada.Equals("POR FACTURAR")) return (0, "Esta consulta no se ha facturado!");

                statusConsulta.Consult_Status = "PROCESADA";

                await this.context_Mysql.SaveChangesAsync();

                return (1, "Consulta procesada correctamente.");
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <summary>
        /// Devuelve la cantidad de consultas de acuerdo al tipo recibido
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public int TotalConsultas(int tipo)
        {
            try
            {
                string[] tipoDeEstados = { "PROCESADA", "PENDIENTE", "CANCELADA" };

                return this.context_Mysql.consultas.Count(x => x.Consult_Status.Equals(tipoDeEstados[tipo - 1]));
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Devuelve la cantidad de consultas por facturar.
        /// </summary>
        /// <returns></returns>
        public int TotalConsultasPorFacturar(int tipo)
        {
            try
            {
                string[] statusPago = { "POR FACTURAR", "FACTURADA" };

                return this.context_Mysql.consultas.Count(x => x.Consult_Facturada.Equals
                (statusPago[tipo-1]) && ( x.Consult_Status.Equals("PENDIENTE") || x.Consult_Status.Equals("PROCESADA")));
            }
            catch ( Exception )
            {
                throw;
            }
        }

    }
}
