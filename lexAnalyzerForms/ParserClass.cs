﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static lexAnalyzerForms.Lexer;
using static lexAnalyzerForms.ParserClass.StateParser;
using static lexAnalyzerForms.OPSTok;

namespace lexAnalyzerForms
{
    public class ParserClass
    {
        public class StateParser
        {
            public StateLetter state;
            public LexemType type;
            public StateParser()
            {
                this.state = StateLetter.F;
                this.type = LexemType.EMPTY;
            }


            public enum StateLetter
            {
                None,
                F,
                I,
                M,
                A,
                N,
                Q,
                S,
                U,
                T,
                V,
                L,
                H,
                C,
                Ъ,
                K,
                D,
                O,
                J,
                B,
                W,
                END
            };

        };


        public class Parser
        {

            public OPSTok oPSTok;
            public List<StateParser> magasin;
            StateParser parsForAdding;
            public Parser()
            {
                magasin = new List<StateParser>();
                oPSTok = new OPSTok();
                //magasin.Add(new StateParser());
                //magasin[0].state = StateLetter.F;
            }


            int i = 0;
            public Lexem Lex()
            {
                return Form1.myStorage[i];
            }
            Lexem lex;
            public void FillMagazine()
            {
                //-while (lex.Type != LexemType.END && magasin[i].state != StateLetter.END)
                lex = Lex(); // берём нулевую лексему из myStorage

                magasin.Add(new StateParser());
                magasin[0].state = StateLetter.F;

                while (lex.Type != LexemType.END)
                {
                    //StateParser stateMagas = new StateParser();
                    //stateMagas = magasin[i];
                    //magasin.RemoveAt(0);

                    if (magasin[i].type != LexemType.END)
                    {
                        //вызываем функцию pair
                        //в pair передавать нулевое состояние магазина 
                        //и тип лексемы, чтобы понимать, какая комбинация - пара
                        Pair(magasin[0].state, lex.Type);
                    }

                    i++;
                    if (i > Form1.myStorage.Count - 1)
                        break;
                    lex = Lex(); // берём следующую лексему из myStorage
                }
            }

            
            public void name()
            {
                oPSTok.ops.Add(lex);
            }
            public void empty()
            {
                return;
            }

            public void Pair(StateLetter stateLex, LexemType type)
            {
                List<StateParser> stateParser = new List<StateParser>();
                List<Action> fucts = new List<Action>(); 

                if (stateLex == StateLetter.F && type == LexemType.NAME)
                {
                    for (int i = 0; i < 6; i++) 
                    { 
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.NAME;
                    stateParser[1].state = StateLetter.H;
                    stateParser[2].type = LexemType.ASSIGN;
                    stateParser[3].state = StateLetter.S;
                    stateParser[4].type = LexemType.SEMICOLON;
                    stateParser[5].state = StateLetter.Q;
                    fucts.Insert(0, name);
                    fucts.Insert(2, name);
                    fucts.Insert(4, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.INT_DECLARE)
                {
                    for (int i = 0; i < 3; i++) 
                    { 
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.INT_DECLARE;
                    stateParser[1].state = StateLetter.I;
                    stateParser[2].state = StateLetter.F;
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.DECIMAL_DECLARE)
                {
                    for (int i = 0; i < 3; i++) 
                    { 
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.DECIMAL_DECLARE;
                    stateParser[1].state = StateLetter.I;
                    stateParser[2].state = StateLetter.F;
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.ARRAY_DECLARE)
                {
                    for (int i = 0; i < 3; i++) 
                    { 
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.ARRAY_DECLARE;
                    stateParser[1].state = StateLetter.A;
                    stateParser[2].state = StateLetter.F;
                    fucts.Insert(0, name);
                }
                if (stateLex == StateLetter.F && type == LexemType.INPUT)
                {
                    for (int i = 0; i < 7; i++) 
                    { 
                        stateParser.Add(new StateParser());
                        fucts.Add(new Action(empty));
                    }
                    stateParser[0].type = LexemType.INPUT;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].type = LexemType.NAME;
                    stateParser[3].state = StateLetter.H;
                    stateParser[4].type = LexemType.RPAREN;
                    stateParser[5].type = LexemType.SEMICOLON;
                    stateParser[6].state = StateLetter.Q;
                    fucts.Insert(0, name);
                    fucts.Insert(1, name);
                    fucts.Insert(2, name);
                    fucts.Insert(4, name);
                    fucts.Insert(5, name);

                }

                magasin.AddRange(stateParser);
            }

            public string printMagasin()
            {
                string text = "MAGASIN\n";
                foreach (StateParser s in magasin) 
                {
                    text += "type: " + s.type.ToString() + '\n';
                    text += "state: " + s.state.ToString() + '\n' + '\n';
                }

                return text;
            }
            public string printGenerator()
            {
                string text = "Generator\n";
                for (int i = 0; i < oPSTok.)
                {
                    text += "type: " + optype.ToString() + '\n';
                }

                return text;
            }

        }
    }
}
