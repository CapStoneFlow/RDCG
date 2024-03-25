using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum unitCode{
    enemy1,
    enemy2,
    boss,
}//각 적을 구분하는 코드.


    public class Status{
        public unitCode unitcode{get;}//get:바꾸지 못하게 고정
        public string name {get; set;}
        public int maxHP{get; set;}
        public int nowHP{get; set;}
        public int atkDmg{get; set;}//기본 공격력(방어로도 사용)
        public int Speed{get;set;}//스피드(행동력)

        public Status(){

        }
        public Status(unitCode unitcode, string name, int maxHP, int nowHP, int atkDmg, int Speed){
            this.unitcode = unitcode;
            this.name = name;
            this.maxHP = maxHP;
            this.nowHP = nowHP;
            this.atkDmg = atkDmg;
            this.Speed = Speed;
        }



       public Status setStatus(unitCode unitcode){
        Status status = null;
        switch(unitcode){
            case unitCode.enemy1:
                status = new Status(unitcode,"적1",10, 10, 3, 5);
                break;

            case unitCode.enemy2:
                status = new Status(unitcode,"적2",20, 20, 6, 6);
                break;

            case unitCode.boss:
                status = new Status(unitcode,"보스",50, 50, 7, 2);
                break;
        }
        return status;
    } 

    }

    
    
    
   

