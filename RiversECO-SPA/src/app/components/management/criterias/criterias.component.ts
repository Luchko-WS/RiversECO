import { Component, OnInit } from '@angular/core';
import { CriteriaService } from 'src/app/services/criteria.service';
import { Criteria } from 'src/app/models/criteria';

@Component({
  selector: 'app-criterias',
  templateUrl: './criterias.component.html',
  styleUrls: ['./criterias.component.css']
})

export class CriteriasComponent implements OnInit {
  criterias: Criteria[];

  constructor(private criteriaService: CriteriaService) {}

  ngOnInit() {
    this.criteriaService.getCriterias()
      .subscribe((res: Criteria[]) => {
        this.criterias = res;
      }, error => {
        console.error(error);
      });
  }
}
