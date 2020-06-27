import { Routes } from '@angular/router';

import { MapComponent } from './components/map/map.component';
import { CriteriasComponent } from './components/management/criterias/criterias.component';

export const appRoutes: Routes = [
    { path: '', component: MapComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            { path: 'management/criterias', component: CriteriasComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
