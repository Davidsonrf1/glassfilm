using GlassFilm.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;


namespace GlassFilm
{
    public partial class FrmSync : Form
    {
        delegate void UpdateStatusDelegate();
        UpdateStatusDelegate _UpdateStatus = null;

        UpdateSyncStatus _syncStatus = null;

        public void UpdateStatusProc()
        {
            if (close)
            {
                //updateThread.Join();
                Close();
            }

            if (verificando)
            {
                if (pb.Value < pb.Maximum)
                    pb.Value++;
            }
            else
            {
                if (pb.Value < pb.Maximum && !verificado)
                {
                    pb.Value = pb.Maximum;
                    Thread.Sleep(500);
                    verificado = true;

                    pb.Style = ProgressBarStyle.Continuous;
                }
                else if (hasUpdate)
                {
                    if (curStatus != null)
                    {
                        pb.Maximum = curStatus.Total;
                        pb.Minimum = 0;

                        if (curStatus.Atual <= pb.Maximum)
                            pb.Value = curStatus.Atual;

                        lbAtualizando.Text = curStatus.Acao;
                    }

                    hasUpdate = false;
                }
            }
        }

        SyncStatus curStatus = null;

        void SyncStatusProc(SyncStatus status)
        {
            curStatus = status;
            hasUpdate = true;
        }

        Dictionary<string, int> syncTables = new Dictionary<string, int>();
        int total = 0;

        bool verificado = false;

        bool forceAll = false;

        public FrmSync()
        {
            InitializeComponent();
            _UpdateStatus = new UpdateStatusDelegate(UpdateStatusProc);
        }

        public Dictionary<string, int> SyncTables
        {
            get
            {
                return syncTables;
            }

            set
            {
                syncTables = value;
            }
        }

        public int Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public bool ForceAll
        {
            get
            {
                return forceAll;
            }

            set
            {
                forceAll = value;
            }
        }

        private void FrmSync_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        bool verificando = false;
        Thread verificThread = null;
        Thread downThread = null;
        Thread upThread = null;
        Thread updateThread = null;

        bool doSyncUp = false;

        void UpdateProc()
        {
            while(!close)
            {
                Invoke(_UpdateStatus);
                Thread.Sleep(50);
            }

            try
            {
                Invoke(_UpdateStatus);
            }
            catch
            {

            }
        }

        void VerificaProc()
        {
            tmrUpdate.Interval = 1000;
            tmrUpdate.Enabled = true;

            Thread.Sleep(1000);

            try
            {
                if (Sync.SyncFullDatabase.VerificaAtualizacoes())
                {
                    total = 10;
                }

                if (total > 0)
                {

                }
            }
            catch
            {

            }
            finally
            {
                tmrUpdate.Interval = 50;
                tmrUpdate.Enabled = true;

                verificando = false;
            }
        }

        void SyncDownProc()
        {
            try
            {
                while (verificando) Thread.Sleep(10);

                if (syncdown && total > 0)
                {
                    SyncFullDatabase.GetDatabase();
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                doSyncUp = true;
            }
        }

        void SyncupProc()
        {
            try
            {
                while (verificando) Thread.Sleep(10);
                while (!doSyncUp) Thread.Sleep(10);

                if (syncup)
                {
                    SyncFullDatabase.SendDatabase();
                }

            }
            catch
            {

            }
            finally
            {
                close = true;
            }
        }

        bool close = false;

        public static void ShowSync(bool up, bool down, bool forceAll)
        {
            try
            {
                FrmSync sync = new FrmSync();

                sync.syncdown = down;
                sync.syncup = up;
                sync.ForceAll = forceAll;

                sync.ShowDialog();
            }
            catch
            {

            }
        }

        bool syncdown = false;
        bool syncup = false;

        private void FrmSync_Shown(object sender, EventArgs e)
        {
            updateThread = new Thread(UpdateProc);
            updateThread.Start();

            _syncStatus = new UpdateSyncStatus(SyncStatusProc);
            SyncManager.SyncStatus = _syncStatus;

            if (syncdown)
            {
                lbAtualizando.Text = "Verificando atualizações...";
                verificando = true;

                pb.Minimum = 0;
                pb.Maximum = 100;
                pb.Value = 0;
                pb.Style = ProgressBarStyle.Marquee;
                pb.MarqueeAnimationSpeed = 500;

                verificThread = new Thread(VerificaProc);
                verificThread.Start();

                Thread.Sleep(1000);
                downThread = new Thread(SyncDownProc);
                downThread.Start();

                upThread = new Thread(SyncupProc);
                upThread.Start();             
            }
            else
            {
                verificando = false;
                doSyncUp = true;

                upThread = new Thread(SyncupProc);
                upThread.Start();
            }
        }

        bool hasUpdate = false;

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {

        }
    }
}
