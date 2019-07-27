using System;

namespace Desafio3_Sequencia_Numeros
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Insira a string desejada: ");
                string str = Console.ReadLine();

                if(question_mark(str))
                    Console.WriteLine("TRUE:    A string '" + str + "' atende a condição.");
                else
                    Console.WriteLine("FALSE:   A string '" + str + "' NÃO atende a condição.");

                // Loop de interação com o usuário
                Console.WriteLine("Deseja testar outra string? S/N");
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

        static bool question_mark(string str)
        {
            // Para cada caractere
            for(int i = 0; i < str.Length; i++)
            {
                // Se é um número
                if(str[i] >= '0' && str[i] <= '9')
                {
                    int firstDigit = Convert.ToInt32(Char.GetNumericValue(str[i]));
                    
                    // Encontra o próximo número
                    for(int j = (i + 1); j < str.Length; j++)
                    {
                        if(str[j] >= '0' && str[j] <= '9')
                        {
                            int nextDigit = Convert.ToInt32(Char.GetNumericValue(str[j]));
                            
                            // Verifica se não possui exatamente 3 interrogações 
                            // caso a soma entre os dois seja maior do que 9. Se não possuir, retorna falso.
                            if(((firstDigit + nextDigit) >= 10) && !HasThreeInterrogations(str, i+1, j-1))
                                return false;
                            
                            break;
                        }
                    }
                }
            }

            return true;
        }

        // Para uma string, verifica se, entre as posições k e l, existem exatamente 3 interrogações
        static bool HasThreeInterrogations(string str, int k, int l)
        {
            int counter = 0;

            for(int i = k; i <= l; i++)
            {
                if(str[i] == '?')
                    counter++;
            }

            if(counter == 3)
                return true;
            return false;
        }
    }
}
