﻿using Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Competencia
    {
        public enum TipoCompetencia
        {
            F1,
            MotoCross
        }

        private short cantidadCompetidores;
        private short cantidadVueltas;
        private List<VehiculoDeCarrera> listaVehiculos;
        private TipoCompetencia tipo;

        #region Properties
        public short CantidadCompetidores
        {
            set
            {
                this.cantidadCompetidores = value;
            }
            get
            {
                return this.cantidadCompetidores;
            }
        }

        public short CantidadVueltas
        {
            set
            {
                this.cantidadVueltas = value;
            }
            get
            {
                return this.cantidadVueltas;
            }
        }

        public List<VehiculoDeCarrera> ListaVehiculos
        {
            get
            {
                return this.listaVehiculos;
            }
        }

        public VehiculoDeCarrera this[int i]
        {
            get
            {
                //if(i >=0 && i < this.listaAutos.Count)
                
                    return this.listaVehiculos[i];//VERIFICAR.
                
            }

            //set
            //{
            //    this.listaAutos[i] = value;
            //}
        }


        public TipoCompetencia Tipo //NO SE QUE ES ESTE ERROR.
        {
            set
            {
                this.tipo = value;
            }
            get
            {
                return this.tipo;
            }
        }
        #endregion


        #region Constructores
        private Competencia()
        {
            listaVehiculos = new List<VehiculoDeCarrera>();
        }

        public Competencia(short cantidadVueltas, short cantidadCompetidores, TipoCompetencia tipo) : this()
        {
            this.CantidadVueltas = cantidadVueltas;
            this.CantidadCompetidores = cantidadCompetidores;
            this.Tipo = tipo;
        }
        #endregion


        #region Métodos
        public string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("---------------------------");
            sb.AppendLine("**--Datos de la competencia--**");
            sb.AppendLine("Cantidad máxima de competidores: " + this.CantidadCompetidores);
            sb.AppendLine("Cantidad de vueltas: " + this.CantidadVueltas);
            sb.AppendLine("---------------------------");
            sb.AppendLine("---------------------------");
            sb.AppendLine("\n   **--COMPETIDORES--**\n");
            sb.AppendLine("---------------------------");
            sb.AppendLine("---------------------------");


            foreach (VehiculoDeCarrera item in this.listaVehiculos)
            {
                if (item is AutoF1)
                {
                    sb.AppendLine(((AutoF1)item).MostrarDatos());
                    sb.AppendLine("---------------------------");

                }
                if (item is MotoCross)
                {
                    sb.AppendLine(((MotoCross)item).MostrarDatos());
                    sb.AppendLine("---------------------------");

                }
            }
            return sb.ToString();
        }
        #endregion


        #region Operaciones
        public static bool operator ==(Competencia competencia, VehiculoDeCarrera vehiculo)
        {
            bool retorno = false;

            //foreach(VehiculoDeCarrera item in competencia.listaAutos)
            //{

            switch (competencia.Tipo)
            {
                case Competencia.TipoCompetencia.MotoCross://si es una motocross
                                                            //if(item is MotoCross)
                    if (vehiculo is MotoCross)
                    {
                        retorno = true;
                    }
                    else
                    {
                        throw new CompetenciaNoDisponibleException($"El vehículo: {vehiculo} no corresponde a la competencia del tipo {competencia.Tipo}",
                            "Competencia.cs", "Competencia == Vehiculo");
                    }
                    break;

                case Competencia.TipoCompetencia.F1://si es una de F1
                                                    //if (item is AutoF1)
                    if (vehiculo is AutoF1)
                    {
                        retorno = true;
                    }
                    else
                    {
                        throw new CompetenciaNoDisponibleException($"El vehículo: {vehiculo} no corresponde a la competencia del tipo {competencia.Tipo}",
                            "Competencia.cs", "Competencia == Vehiculo");
                    }
                    break;
            }
            
            
                //throw new CompetenciaNoDisponibleException("El vehiculo no corresponde a la competencia",
                //                "Clase:" + ex.NombreClase, "Método: " + ex.NombreMetodo,ex);


            return retorno;
        }


        public static bool operator !=(Competencia competencia, VehiculoDeCarrera vehiculo)
        {
            return !(competencia == vehiculo);
        }


        public static bool operator +(Competencia competencia, VehiculoDeCarrera vehiculo)
        {
            bool retorno = false;
            Random numRandom = new Random();

            try
            {
                if (competencia == vehiculo)
                {
                    if (competencia.listaVehiculos.Count < competencia.CantidadCompetidores)
                    {
                        if (!(competencia.listaVehiculos.Contains(vehiculo)))
                        {
                            competencia.listaVehiculos.Add(vehiculo);

                            vehiculo.EnCompetencia = true;
                            vehiculo.VueltasRestantes = competencia.CantidadVueltas;
                            vehiculo.CantidadCombustible = (short)numRandom.Next(15, 100);
                            retorno = true;
                        }
                    }
                }
            }
            catch (CompetenciaNoDisponibleException ex)
            {
                CompetenciaNoDisponibleException aux = new CompetenciaNoDisponibleException(ex.Message,
                    "Clase: " + ex.NombreClase, "Método: " + ex.NombreMetodo, ex);

                throw aux;
            }
            
            return retorno;
        }
        //public static bool operator +(Competencia competencia, AutoF1 auto)
        //{
        //    bool retorno = false;
        //    Random numRandom = new Random();

        //    if (competencia.listaAutos.Count < competencia.CantidadCompetidores)
        //    {
        //        if (!(competencia.listaAutos.Contains(auto)))
        //        {
        //            competencia.listaAutos.Add(auto);

        //            auto.EnCompetencia = true;
        //            auto.VueltasRestantes = competencia.CantidadVueltas;
        //            auto.CantidadCombustible = (short)numRandom.Next(15, 100);

        //            retorno = true;
        //        }
        //    }
        //    return retorno;
        //}

        public static bool operator -(Competencia competencia, AutoF1 auto)
        {
            bool retorno = false;

            if (competencia.listaVehiculos.Contains(auto))
            {
                competencia.listaVehiculos.Remove(auto);
                retorno = true;
            }

            return retorno;
        }
        #endregion

    }
}
