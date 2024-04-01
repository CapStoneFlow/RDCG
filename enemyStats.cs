using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
이후 적 디자인, 밸런스 분배 필요*/
public enum unitCode{
    enemy1,
    enemy2,
    boss,
}//각 적을 구분하는 코드.


    public class enemyStats{
        public unitCode unitcode{get;}//get:바꾸지 못하게 고정
        public string name {get; set;}//enemy 이름
        public int maxHP{get; set;}//최대 hp
        public int nowHP{get; set;}//현재 hp
        public int maxAtkDmg{get; set;}//공격력(방어력으로도 사용)최대
        public int minAtkDmg{get; set;}//공격력(=방어력)최소
        

        public enemyStats(){

        }
        public enemyStats(unitCode unitcode, string name, int maxHP, int nowHP, int minAtkDmg, int maxAtkDmg){
            this.unitcode = unitcode;
            this.name = name;
            this.maxHP = maxHP;
            this.nowHP = nowHP;
            this.maxAtkDmg = maxAtkDmg;
            this.minAtkDmg = minAtkDmg;
            
        }



       public enemyStats setStatus(unitCode unitcode){
        enemyStats status = null;
        switch(unitcode){
            case unitCode.enemy1:
                status = new enemyStats(unitcode,"적1",10, 10, 1,3);
                break;

            case unitCode.enemy2:
                status = new enemyStats(unitcode,"적2",20, 20, 4,7);
                break;

            case unitCode.boss:
                status = new enemyStats(unitcode,"보스",50, 50, 5,10);
                break;
        }
        return status;
    }//unitcode에 따라 적을 지정하는 함수. 이후 적을 추가/제거하거나 스탯,이름 변경이 가능함 

    }

    
    
    
   

