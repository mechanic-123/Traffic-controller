﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_CRS.Models;
using TMS_CRS.DAL;

namespace TMS_CRS.DAL
{
    public class RTOImpl : IRTO
    {
        readonly TMSDBContext db;
        public RTOImpl()
        {
            db = new TMSDBContext();
        }
        public RTOImpl(TMSDBContext db)
        {
            this.db = db;
        }

        public int AddOwner(TmOwnerdetail o)
        {
            db.TmOwnerdetails.Add(o);
            var res = db.SaveChanges();
            if (res == 1)
                return db.TmOwnerdetails.Max(x => x.OwnerId);
            else
                return 0;
        }

        public int AddVechile(TmVehicledetail v)
        {
            db.TmVehicledetails.Add(v);
            var res = db.SaveChanges();
            if (res == 1)
                return db.TmVehicledetails.Max(x => x.VehId);
            else
                return 0;
        }
        public List<TmRegdetail> GenerateReport(int id)
        {
            return db.TmRegdetails.Where(x => x.OwnerId == id).ToList();
        }

        public List<TmRegdetail> GetAll()
        {
            return db.TmRegdetails.ToList();
        }

        public TmRegdetail GetById(string appno)
        {
            return db.TmRegdetails.Where(x => x.AppNo == appno).FirstOrDefault();
        }

        public int Registration(TmRegdetail r)
        {
            db.TmRegdetails.Add(r);
            var res = db.SaveChanges();
            if (res == 1)
                return 1;
            else
                return 0;
        }

        public bool Transferdetails(TmRegdetail r, string appno)
        {
            var olddata = db.TmRegdetails.Where(x => x.AppNo == appno).FirstOrDefault();
            olddata.OldOwnerId = olddata.OwnerId;
            olddata.OwnerId = r.OwnerId;
            db.Update(olddata);
            var res = db.SaveChanges();
            if (res == 1)
                return true;
            else
                return false;
        }
    }
}
