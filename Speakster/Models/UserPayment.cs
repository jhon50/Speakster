using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Speakster.Models
{
    public class UserPayment
    {
        /* User_id
         * Cada usuário possui um registro de pagamento,
         * tal registro é identificado pelo User_id
         * que no caso é o ID principal do usuário
         * independentemente de sua ROLE.
         */
        [Key]
        public string User_id { get; set; }

        /* payment_date
         * Registro com a data do último pagamento
         * realizado. Seja um novo pagamento, ou
         * pagamentos subsequentes ao inicio da assinatura
         * 
         */
        public string Payment_date { get; set; }

        /* payment_status
        * Registra se a ordem do pagamento está
        * "Completed". Possui diversos estados.
        * 
        */
        public string Payment_status { get; set; }


        /* subscr_date
         * Start date or cancellation date depending on
         *  whether transaction is "subscr_signup" 
         *  or "subscr_cancel"
         */
        public string Subscr_date { get; set; }

       /* txn_type
       * Este atributo é responsável pelo transaction_type
       * ou seja, o tipo da transação. Em alguns dos casos possíveis
       * o usuário pode estar realizando uma nova assinatura ou 
       * cancelando sua assinatura no site do PayPal.
        */
        public string Txn_type { get; set; }

        /* txn_id
         * Este atributo possui o ID da transação.
         * Por questões de segurança é preciso verificar
         * se já existe alguma transação com o mesmo ID,
         * para que não ocorram pagamentos em duplicidade.
         */
        [Index(IsUnique = true)]
        public string Txn_id { get; set; }


        public string Payer_email { get; set; }

        public string Receiver_email { get; set; }

        public string First_name { get; set; }

        public string Last_name { get; set; }

        public string Address_city { get; set; }

        public string Address_country { get; set; }

        public string Address_state { get; set; }

        public string Address_status { get; set; }

        public string Address_zip { get; set; }

        public bool isActive()
        {
            return Payment_status.Equals("Completed") ? true : false;
        }

    }
}