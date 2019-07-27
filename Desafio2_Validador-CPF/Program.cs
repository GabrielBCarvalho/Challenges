using System;
using System.Text.RegularExpressions;

namespace Desafio2_Validador_CPF
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true){
                Console.WriteLine("Digite o número do CPF:");
                string originalCPF, cpf;

                // Verifica se, inicialmente, a string é válida, sendo possivelmente um CPF válido
                // Além disso, retira os caracteres especiais, passando a trabalhar com uma string 
                // contendo apenas dígitos
                while(true){
                    originalCPF = Console.ReadLine();
                    cpf = Regex.Replace(originalCPF, "[^0-9]", "");

                    if(!String.IsNullOrEmpty(cpf) && cpf.Length != 11)
                        Console.WriteLine("Por favor, digite um CPF válido.");
                    else
                        break;
                }
                
                // Chamada da função de validação e exibição da mensagem de resposta
                if(IsValidCPF(cpf))
                    Console.WriteLine("O CPF " + originalCPF + " é um CPF válido.");
                else
                    Console.WriteLine("O CPF " + originalCPF + " não é um CPF válido.");
                
                // Loop de interação com o usuário
                Console.WriteLine("Deseja testar outro CPF? S/N");
                string answer;
                while(true){
                    answer = Console.ReadLine();
                    if(answer == "N" || answer == "n" || answer == "S" || answer == "s")
                        break;
                }

                if(answer == "N" || answer == "n")
                    break;
            }
            
        }

        // Função principal da checagem de um CPF válido ou não
        static bool IsValidCPF(string strCPF)
        {
            // Converte string do CPF para array de inteiros
            int[] cpf = new int[11];
            for (int i = 0; i < 11; i++){
                cpf[i] = Convert.ToInt32(Char.GetNumericValue(strCPF[i]));
            }

            // Variáveis auxiliares
            int sum;
            int firstVerifierDigit = cpf[9];
            int secondVerifierDigit = cpf[10];
            
            // Passos 1 a 3 (soma da multiplicação dos valores na mesma coluna)
            sum = GetSum(cpf, 9, 10);

            // Checa o primeiro dígito verificador. Se consistir, continua (passos 4 a 6)
            if(CheckVerifierDigit(sum, firstVerifierDigit)){
                // Passos 7 a 10 (soma da multiplicação dos valores na mesma coluna)
                sum = GetSum(cpf, 10, 11);

                // Passos 11 a 13: analisa o último dígito verificador
                if(CheckVerifierDigit(sum, secondVerifierDigit))
                    return true;
            }

            return false;
        }

        // Retorna a soma da multiplicação entre um número na primeira linha e outro na segunda linha, ambos na mesma coluna.
        // Passos 1 a 3 ou 7 a 9
        static int GetSum(int[] cpf, int finalI, int initialJ)
        {
            int sum = 0;
            int j = initialJ;

            for(int i = 0; i < finalI; i++){
                sum += cpf[i] * j;
                j--;
            }

            return sum;
        }

        // Obtém o dígito de checagem do algoritmo e compara com o dígito verificador
        static bool CheckVerifierDigit(int sum, int verifierDigit)
        {
            int checker = 11 - (sum % 11);
            if((checker > 9 && verifierDigit == 0) || (checker <= 9 && (checker == verifierDigit)))
                return true;
            return false;
        }
    }   
}
