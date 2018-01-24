import { Routes, RouterModule } from '@angular/router';

// Service
import { RoleGuardService } from '../../shared/index.shared';

// Component
import { CandidateComponent } from '../index.candidate';

const CANDIDATE_ROUTES: Routes = [
    {
        path: 'Candidate',
        component: CandidateComponent,
        canActivate: [RoleGuardService],
        data: { expectedRole: 'Admin' }
    },
    {
        path: 'Candidate/:candidateId',
        component: CandidateComponent,
        canActivate: [RoleGuardService],
        data: { expectedRole: 'Admin' }
    }
];

export const CandidateRouterModule = RouterModule.forRoot(CANDIDATE_ROUTES, { useHash: false });
