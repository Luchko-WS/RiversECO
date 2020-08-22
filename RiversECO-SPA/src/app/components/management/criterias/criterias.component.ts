import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { CriteriaService } from 'src/app/services/criteria.service';
import { Criteria } from 'src/app/models/criteria';

@Component({
  selector: 'app-criterias',
  templateUrl: './criterias.component.html',
  styleUrls: ['./criterias.component.css']
})

export class CriteriasComponent implements OnInit {
  criterias: Criteria[];

  constructor(
    private route: ActivatedRoute,
    private criteriaService: CriteriaService) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.criterias = data['criterias'];
    });
  }
}
