import { Component, OnInit } from '@angular/core';

@Component({
    template: `<h1>Admin</h1>
    <chart type="pie" [data]="data"></chart>
    `
})

export class AdminComponent implements OnInit {
    constructor(){}

    // ONLY TO SHOW HOW TO USE CHARTS !!!! SIMPLE EXAMPLE

    data = {
        labels: ['Audi', 'Bmw', 'Ford'],
        datasets: [
            {
                data: [5, 6, 7],
                backgroundColor: ["#ff6384","#36a2eb","#ffce56"]
            }
        ]
    };

    ngOnInit(){}
}